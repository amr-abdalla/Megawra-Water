# State Machine Diagram
```mermaid
stateDiagram-v2
	[*] --> Idle

	SmallFall --> Landing
	SmallFall --> Idle
	SmallJump --> SmallFall
	SmallJump --> Fall
	Landing --> Launch
	Landing --> SuperLaunch
	Dive --> GlideStart
	Dive --> Fall
	Fall --> Dive
	Fall --> Landing
	Fall --> GlideStart
	Fall --> Crash
	Walk --> Idle
	Walk --> Bump
	Walk --> SmallLaunch
	Crash --> Launch
	Crash --> Idle
	Crash --> SuperLaunch
	Glide --> Fall
	GlideStart --> Glide
	SmallLaunch --> SmallJump
	SuperLaunch --> Jump
	Jump --> Dive
	Jump --> Fall
	Launch --> Jump
	Bump --> Idle
	Idle --> Walk
	Idle --> SmallLaunch

```