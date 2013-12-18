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

        public static string name="noname";
        public static bool showHealthbars = true;

        #endregion

        #region Initialization

        enum Sound {On,Off}

        Sound currentSoundOption;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Optionen")
        {
            // Create our menu entries.
            playername = new MenuEntry(string.Empty);
            sound = new MenuEntry(string.Empty);
            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            



            // Hook up menu event handlers.
            back.Selected += OnCancel;
            sound.Selected += ToggleSound;
            // Add entries to the menu.
            MenuEntries.Add(sound);
         //   MenuEntries.Add(playername);
            MenuEntries.Add(back);
        }


        void ToggleSound(object sender, PlayerIndexEventArgs e)
        {
            if (currentSoundOption == Sound.On)
            {
                currentSoundOption = Sound.Off;
            }
            else
            {
                currentSoundOption = Sound.On;
            }
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            playername.Text = "Spielername: " + name;
            sound.Text = "Sound:" + currentSoundOption;
        }

        
        #endregion

    }
}
