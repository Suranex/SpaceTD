#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using GameStateManagementSample.Logic;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry playername;
        MenuEntry sound;
        MenuEntry healthbar;

        public static string name="noname";
        public static bool showHealthbars = true;

        #endregion

        #region Initialization

        enum Health { Lebensbalken, Rotfaerbung}
        Health currentHealthOption;

        enum Sound {An,Aus}
        Sound currentSoundOption;


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Optionen")
        {
            // Create our menu entries.
            sound = new MenuEntry(string.Empty);
            healthbar = new MenuEntry(string.Empty);
            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            back.Selected += OnCancel;
            sound.Selected += ToggleSound;
            healthbar.Selected += ToggleHealthBar;
            // Add entries to the menu.
            MenuEntries.Add(sound);
            MenuEntries.Add(healthbar);
            MenuEntries.Add(back);
        }

        void ToggleHealthBar(object sender, PlayerIndexEventArgs e)
        {
            if (currentHealthOption == Health.Lebensbalken)
            {
                currentHealthOption = Health.Rotfaerbung;
                OptionsMenuScreen.showHealthbars = false;
            }
            else
            {
                currentHealthOption = Health.Lebensbalken;
                OptionsMenuScreen.showHealthbars = true;
            }
            SetMenuEntryText();
        }

        void ToggleSound(object sender, PlayerIndexEventArgs e)
        {
            if (currentSoundOption == Sound.An)
            {
                currentSoundOption = Sound.Aus;
                GameMenuRight.sound = false;
            }
            else
            {
                currentSoundOption = Sound.An;
                GameMenuRight.sound = true;
            }
            SetMenuEntryText();
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            sound.Text = "Sound: " + currentSoundOption;
            healthbar.Text="Lebensanzeige: " + currentHealthOption;
        }

        
        #endregion

    }
}
