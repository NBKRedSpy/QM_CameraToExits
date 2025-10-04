[h1]Quasimorph Camera To Exits[/h1]


Adds a hotkey to move the camera view to the current level's exits.  Helpful when the player is moving loot back to the shuttle from multiple floors.

Press left bracket for move to the down elevator or the drop pod.  Press right bracket to move to the up elevator or shuttle.

The camera will only move if the target has already been explored.

The hotkeys can be configured.

[h1]Configuration[/h1]

This mod includes MCM configuration.  Some values can be edited in the Mods screen, while the rest will be display only. All options can be configured in the config file.

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\CameraToExits\config.json[/i].
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]MoveToDownElevatorKey
[/td]
[td]Left Bracket
[/td]
[td]When pressed, moves to the elevator that goes down.
[/td]
[/tr]
[tr]
[td]MoveToUpElevatorKey
[/td]
[td]Right Bracket
[/td]
[td]When pressed, moves to the elevator that goes up or the shuttle.
[/td]
[/tr]
[tr]
[td]CycleMilliseconds
[/td]
[td]1000
[/td]
[td]he minimum amount of time that the user has to press the same key to cycle between targets.
[/td]
[/tr]
[/table]

[h2]Key List[/h2]

The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

[h1]Buy Me a Coffee[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Credits[/h1]
[list]
[*]Special thanks to Crynano for his excellent Mod Configuration Menu.
[/list]

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/QM_CameraToExits

[h1]Change Log[/h1]

[h2]1.2.3[/h2]
[list]
[*]Beta compatibility.
[/list]

[h2]1.2.2[/h2]

Fix: Now only activates if no window is shown.

[h2]1.2.1[/h2]
[list]
[*]Fix for MCM not being bypassed due to Mono specific bug.
[/list]

[h2]1.2.0[/h2]
[list]
[*]MCM Integration.
[/list]

[h2]1.1.0[/h2]
[list]
[*]Added drop pod to the list of items.  The user can press the "down key" again to cycle between the exist and the drop pod.  Intentionally includes if the area is not explored since it is shown on the minimap.
[/list]
