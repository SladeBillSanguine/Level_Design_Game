// ================================================================
[BUNTILITY_SET]

// ................................................................
[ABSTRACT]
This Set offers a few Components which are supposed to be a general Framework for Basic Tasks.


// ................................................................
[PRE_INSTALLS]
IMPORTANT: Make sure to install the following Assets before adding this Prefab!

= InputSystem
= DOTween
= Cinemachine (optional)
= TMPro (optional)
= Universal Render Pipeline (optional)


// ................................................................
[OVERVIEW]
= CoreApp				: Starts the Game
	= GameStateHub			: Transition between Intro / MainMenu / Game
	= Basic Menu			: Menu Framework StartGame / Options / Credits / Quit
	= SceneLoader			: Loads Scenes
	= IntroLoader			: Handles the Intro
= FTBHandler			: Triggers FadeToBlack or Cinematic Bars
= InputHub				: Basic Hub for UnityEngine.InputSystem
= RoutineHub			: Supplies Routines with Callbacks, which can be called from static classes
= TagHub				: A Hub to save all Tags
= IsoTimeLine			: Isolated Timeline Handler, which can trigger Timelines + Cinemachine Cams


// ................................................................
[SETUP]
[1] Create a Scene which will NOT be destroyed
[2] Add Buntility_Hub.prefab
[3] Done