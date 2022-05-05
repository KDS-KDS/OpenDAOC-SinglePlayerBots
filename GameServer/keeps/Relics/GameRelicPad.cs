using System;
using System.Collections;
using System.Collections.Generic;
using DOL.Events;
using DOL.Database;
using DOL.GS;
using DOL.GS.PacketHandler;
using DOL.Language;
using System.Reflection;
using log4net;

namespace DOL.GS
{
	public class GameRelicPad : GameStaticItem
	{
		const int PAD_AREA_RADIUS = 250;

		PadArea m_area = null;
		GameRelic m_mountedRelic = null;

		#region constructor
		public GameRelicPad()
			: base()
		{
		}
		#endregion

		#region Add/remove from world
		/// <summary>
		/// add the relicpad to world
		/// </summary>
		/// <returns></returns>
		public override bool AddToWorld()
		{
			m_area = new PadArea(this);
			CurrentRegion.AddArea(m_area);
			bool success = base.AddToWorld();
			if (success)
			{
				/*
				 * <[RF][BF]Cerian> mid: mjolnerr faste (str)
					<[RF][BF]Cerian> mjollnerr
					<[RF][BF]Cerian> grallarhorn faste (magic)
					<[RF][BF]Cerian> alb: Castle Excalibur (str)
					<[RF][BF]Cerian> Castle Myrddin (magic)
					<[RF][BF]Cerian> Hib: Dun Lamfhota (str), Dun Dagda (magic)
				 */
				//Name = GlobalConstants.RealmToName((DOL.GS.PacketHandler.eRealm)Realm)+ " Relic Pad";
				RelicMgr.AddRelicPad(this);
			}

			return success;
		}

		public override ushort Model
		{
			get
			{
				return 2655;
			}
			set
			{
				base.Model = value;
			}
		}

		public override eRealm Realm
		{
			get
			{
				switch (Emblem)
				{
					case 1:
					case 11:
						return eRealm.Albion;
					case 2:
					case 12:
						return eRealm.Midgard;
					case 3:
					case 13:
						return eRealm.Hibernia;
					default:
						return eRealm.None;
				}
			}
			set
			{
				base.Realm = value;
			}
		}

		public virtual eRelicType PadType
		{
			get
			{
				switch (Emblem)
				{
					case 1:
					case 2:
					case 3:
						return eRelicType.Strength;
					case 11:
					case 12:
					case 13:
						return eRelicType.Magic;
					default:
						return eRelicType.Invalid;

				}
			}
		}

		/// <summary>
		/// removes the relicpad from the world
		/// </summary>
		/// <returns></returns>
		public override bool RemoveFromWorld()
		{
			if (m_area != null)
				CurrentRegion.RemoveArea(m_area);

			return base.RemoveFromWorld();
		}
		#endregion

		/// <summary>
		/// Checks if a GameRelic is mounted at this GameRelicPad
		/// </summary>
		/// <param name="relic"></param>
		/// <returns></returns>
		public bool IsMountedHere(GameRelic relic)
		{
			return m_mountedRelic == relic;
		}

		public void MountRelic(GameRelic relic, bool returning)
		{
			m_mountedRelic = relic;

			if (relic.CurrentCarrier != null && returning == false)
			{
				/* Sending broadcast */
				string message = LanguageMgr.GetTranslation(ServerProperties.Properties.SERV_LANGUAGE, "GameRelicPad.MountRelic.Stored", relic.CurrentCarrier.Name, GlobalConstants.RealmToName((eRealm)relic.CurrentCarrier.Realm), relic.Name, Name);
				foreach (GameClient cl in WorldMgr.GetAllPlayingClients())
				{
					if (cl.Player.ObjectState != eObjectState.Active) continue;
                    cl.Out.SendMessage(LanguageMgr.GetTranslation(cl.Account.Language, "GameRelicPad.MountRelic.Captured", GlobalConstants.RealmToName((eRealm)relic.CurrentCarrier.Realm), relic.Name), eChatType.CT_ScreenCenterSmaller, eChatLoc.CL_SystemWindow);
					cl.Out.SendMessage(message + "\n" + message + "\n" + message, eChatType.CT_Important, eChatLoc.CL_SystemWindow);
				}
				NewsMgr.CreateNews(message, relic.CurrentCarrier.Realm, eNewsType.RvRGlobal, false);

				/* Increasing of CapturedRelics */
				//select targets to increase CapturedRelics
				//TODO increase stats

				List<GamePlayer> targets = new List<GamePlayer>();
				if (relic.CurrentCarrier.Group != null)
				{
					foreach (GamePlayer p in relic.CurrentCarrier.Group.GetPlayersInTheGroup())
					{
						targets.Add(p);
					}
				}
				else
				{
					targets.Add(relic.CurrentCarrier);
				}

				foreach (GamePlayer target in targets)
				{
					target.CapturedRelics++;
					target.RaiseRealmLoyaltyFloor(2);
					target.Achieve(AchievementUtils.AchievementNames.Relic_Captures);
				}

				Notify(RelicPadEvent.RelicMounted, this, new RelicPadEventArgs(relic.CurrentCarrier, relic));
			}
			else
			{
				// relic returned to pad, probably because it was dropped on ground and timer expired.
				string message = string.Format("The {0} has been returned to {1}.", relic.Name, Name);
				foreach (GameClient cl in WorldMgr.GetAllPlayingClients())
				{
					if (cl.Player.ObjectState != eObjectState.Active) continue;
					cl.Out.SendMessage(message + "\n" + message + "\n" + message, eChatType.CT_Important, eChatLoc.CL_SystemWindow);
				}

			}
		}

		public void RemoveRelic(GameRelic relic)
		{
			m_mountedRelic = null;

			if (relic.CurrentCarrier != null)
			{
				string message = LanguageMgr.GetTranslation(ServerProperties.Properties.SERV_LANGUAGE, "GameRelicPad.RemoveRelic.Removed", relic.CurrentCarrier.Name, GlobalConstants.RealmToName((eRealm)relic.CurrentCarrier.Realm), relic.Name, Name);
				foreach (GameClient cl in WorldMgr.GetAllPlayingClients())
				{
					if (cl.Player.ObjectState != eObjectState.Active) continue;
					cl.Out.SendMessage(message + "\n" + message + "\n" + message, eChatType.CT_Important, eChatLoc.CL_SystemWindow);
				}
				NewsMgr.CreateNews(message, relic.CurrentCarrier.Realm, eNewsType.RvRGlobal, false);

				Notify(RelicPadEvent.RelicStolen, this, new RelicPadEventArgs(relic.CurrentCarrier, relic));
			}
		}
		
		public int GetEnemiesOnPad()
		{
			var players = GetPlayersInRadius(500);
			var enemyNearby = 0;
				
			foreach (GamePlayer p in players)
			{
				if (p.Realm == Realm) continue;
				enemyNearby++;
			}
			return enemyNearby;
		}

		public void RemoveRelic()
		{
			m_mountedRelic = null;
		}

		public GameRelic MountedRelic
		{
			get { return m_mountedRelic; }
		}

		/// <summary>
		/// Area around the pit that checks if a player brings a GameRelic
		/// </summary>
		public class PadArea : Area.Circle
		{
			private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

			GameRelicPad m_parent;

			public PadArea(GameRelicPad parentPad)
				: base("", parentPad.X, parentPad.Y, parentPad.Z, PAD_AREA_RADIUS)
			{
				m_parent = parentPad;
			}

			public override void OnPlayerEnter(GamePlayer player)
			{
				GameRelic relicOnPlayer = player.TempProperties.getProperty<object>(GameRelic.PLAYER_CARRY_RELIC_WEAK, null) as GameRelic;
				if (relicOnPlayer == null)
				{
					return;
				}

				if (relicOnPlayer.RelicType != m_parent.PadType || m_parent.MountedRelic != null)
				{
                    player.Client.Out.SendMessage(string.Format(LanguageMgr.GetTranslation(player.Client.Account.Language, "GameRelicPad.OnPlayerEnter.EmptyRelicPad"), relicOnPlayer.RelicType), eChatType.CT_Important, eChatLoc.CL_SystemWindow);
					log.DebugFormat("Player {0} needs to find an empty {1} relic pad in order to place {2}.", player.Name, relicOnPlayer.RelicType, relicOnPlayer.Name);
					return;
				}

				if (player.Realm == m_parent.Realm)
				{
					log.DebugFormat("Player {0} captured relic {1}.", player.Name, relicOnPlayer.Name);
					relicOnPlayer.RelicPadTakesOver(m_parent, false);
				}
				else
				{
					log.DebugFormat("Player realm {0} wrong realm on attempt to capture relic {1} of realm {2} on pad of realm {3}.",
					                GlobalConstants.RealmToName(player.Realm),
					                relicOnPlayer.Name,
					                GlobalConstants.RealmToName(relicOnPlayer.Realm),
					                GlobalConstants.RealmToName(m_parent.Realm));
				}
			}
		}

	}
}
