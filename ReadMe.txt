This is Jeff Miller's submission of the Mars Rover test project for DealerOn as a Development Candidate.

This file serves as the requirement for explaination of assumptions and etc.

DESIGN & ASSUMPTIONS:
Primary assumptions in providing this code sample is around the lack of specified behavior for error handling.
As a result of the above, pessimistic behavior is standard in the case of errors.
	In the case of multiple rover command sets, rovers will quit operation independent of one another and stop in place.
	Any higher level errors will quit the entire mission prematurely.
	Catastrophic errors (errors that end the mission execution prematurely) will report that error.

As there was no specification for coding, handling, or otherwise considering delivery or retrieval of the rovers,
they are treated as "warp in, warp out" for the purposes of this code, there is no persistant referencing of rovers between
instruction sets.  All rovers exist at their starting points, conduct their missions, and will then all be returned.

Rovers will always report their final position and heading, regardless of success, unless they failed to initialize.

CAVEATS:
Additional code was added both for the purpose of just giving an excuse to demonstrate code capability, write against "anticipate demand", 
and with respect to the assumption that the NASA communication protocol precedes the rover.
Expanding the protocol to enable these features without enaging with the communication engineers would not be a normal step of development.

The aformentioned extension code is intended to support for:
	Allowing a set of commands to be treated as being Optimistic, so that if a rover fails to enact a command, it will continue until it runs out of commands.
	Allowing for debug option to return what errors are encountered during execution.
	In the event of both Optimistic and Debug, it'll provide only the most recent error encountered.

Additionally, since there is no "return protocol", console output is considered simulated, but the raw results are available.