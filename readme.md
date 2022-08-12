# RocketLanding
## Description
<p>Library used to create a rocket Landing Area which allows several rocket to check if a certain landing position is correct.</p>

## Exposed classes
- <b>Landing Area</b>
<p>
<p>Defines an area in which rockets can land. It provides a method "CheckLandingPosition" that checks if the position asked by a rocket is correct</p>
<p>The following use cases have been taken into account:<p/>
<p>1. If the rocket asks for a position outside the platform -> Return 'out of platform'.</p>
<p>2. If the rocket checks for a position that has been checked by the previous rocket -> Return 'clash'. </p>
<p>3. If the rocket checks for a position that is one unit close to a position that has been checked by any previous different rocket -> Return 'clash'.</p>
<p>4. If a position is set as "danger position" because it is surrounding a checked position, and the same rocket that defines that danger position checks on the danger position, the result will be 'ok for landing' and a new "danger zone" will be defined.</p>
<p>5. Only the last valid checked position counts for each rocket.</p></p>

- <b>Landing Area Builder</b>
<p>
Provides a way to easily and safely build a Landing Area. It allows to set the Landing Area Size, the Platform Area Size and the Platform Area Position.
</p>

## Unit testing
<p>Automated unit tests are provided in the project RocketLanding.Test for the LandingArea and LandingAreaBuilder classes.</p>
