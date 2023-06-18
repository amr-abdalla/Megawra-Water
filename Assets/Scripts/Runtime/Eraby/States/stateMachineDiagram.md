# State Machine Diagram

```mermaid
stateDiagram-v2
	[*] --> Idle

	Jump --> Dive
	Jump --> Fall
	Dive --> GlideStart
	Dive --> Fall
	Launch --> Jump
	GlideStart --> Glide
	Bump --> Idle
	Crash --> Idle
	Crash --> Launch
	Crash --> Fall
	SmallLaunch --> SmallJump
	Walk --> Idle
	Walk --> SmallLaunch
	Walk --> Bump
	Idle --> SmallLaunch
	Idle --> Walk
	Glide --> Fall
	Bounce --> Launch
	Bounce --> Crash
	Bounce --> Fall
	SmallJump --> SmallFall
	SmallJump --> Fall
	SmallFall --> Idle
	SmallFall --> Bounce
	Fall --> GlideStart
	Fall --> Dive
	Fall --> Bounce
	Fall --> Crash

```
