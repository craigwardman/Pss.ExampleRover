Feature: Rover Controls
	In order to control a rover,
	As a NASA remote Rover operator
	I want to be able to send simple strings of letters.

Scenario: Simple forward and right turn example
	Given the rover is located at 0,0
	And the rover is facing North
	And the grid is 100x100
	When the command sequence is "FFRFF"
	Then the rover should be located at 2,2
	And the rover should be facing East

Scenario: Y-Axis Grid wrapping example
	Given the rover is located at 99,99
	And the rover is facing North
	And the grid is 100x100
	When the command sequence is "F"
	Then the rover should be located at 99,0

Scenario: Y-Axis Grid reverse wrapping example
	Given the rover is located at 99,0
	And the rover is facing North
	And the grid is 100x100
	When the command sequence is "B"
	Then the rover should be located at 99,99

Scenario: X-Axis Grid wrapping example
	Given the rover is located at 99,99
	And the rover is facing East
	And the grid is 100x100
	When the command sequence is "F"
	Then the rover should be located at 0,99

Scenario: X-Axis Grid reverse wrapping example
	Given the rover is located at 0,99
	And the rover is facing East
	And the grid is 100x100
	When the command sequence is "B"
	Then the rover should be located at 99,99

Scenario Outline: Spin examples
	Given the rover is facing <Heading>
	When the command sequence is "<Turn>"
	Then the rover should be facing <New Heading>

	Examples:
		| Heading | Turn | New Heading |
		| North   | R    | East        |
		| North   | L    | West        |
		| East    | R    | South       |
		| East    | L    | North       |
		| West    | R    | North       |
		| West    | L    | South       |
		| South   | R    | West        |
		| South   | L    | East        |

Scenario: Obstacle Detection
	Given the rover is located at 0,0
	And there is an obstacle at 0,2
	And the rover is facing North
	When the command sequence is "FF"
	Then the rover should be located at 0,1
	And the rover returned an obstacle blockage message