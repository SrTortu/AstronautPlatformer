# Change Log 14-02-2025:

**Code:**

- Formatting errors corrected
- Variable and script names corrected
- Regions properly organized
- Improved code consistency regarding syntax
- Removed unnecessary comments
- Removed dead code
- Improved component capturing

**Editor:**

- Organized the Assets folder
- Fixed animation scheme

**Gameplay:**

- Improved player animation when picking up a negative item
- Added visual aids for the grappling hook, now if something is within range, it lights up using Light2D, and the mouse cursor changes when hovering over a platform that is in range
- Improved item size and falling speed
- The grappling hook rope now has a texture
- Added an ending

# Change Log 09-02-2025:

**Player:**

- The player can now pass through platforms from below
- The player can now run (using movement keys)
- "Fly" has been changed to "Jump" (Space key)
- The player now looks toward the relative position of the mouse
- Implemented grappling hook gun
- Added a visual and sound alert when the player doesn't have enough energy to jump
- Jumping now has sound
- Added animation for the negative effects of items

**Grappling Hook Gun:** The gun shoots a hook (left-click) that attaches to a platform if it's within 10m or less

- The player's gun now points relative to the mouse position
- Jumping removes the hook from the platform
- Right-click removes the hook (no utility)
- If the platform disappears, the hook does too
- If the platform moves, the hook maintains its position on the platform
- Added shooting sound

**Platforms:** Added special platforms (Levels 3, 4, and 5)

- Level 3 platforms disappear and reappear at random intervals
- Level 4 platforms move along the X-axis between two points A and B
  - The player can stand on Level 4 platforms and move along with them
- Level 5 platforms also move but have a specific material with low friction

**Environment:**

- Added lighting to levels
- Changed "InGame" music (now "Inner Station" from Metal Slug)
- Added intro music to the Main Menu (Intro from Contra)

**Items:** Reworked all items, including functionality, sprites, particles, and sounds

- Bomb Item: Gives the player a negative impulse on the Y-axis and paralyzes them for 1 second. If the player is hooked to a platform, the hook is removed
- Skull Item: Decreases energy regeneration rate by -0.1 and paralyzes the player for 1 second
- Water Item: Increases energy regeneration rate by 0.1
- Items are now properly removed when touching the ground
