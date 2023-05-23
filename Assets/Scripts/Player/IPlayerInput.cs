using System;

public interface IPlayerInput
{
	event Action AttackEvent;
	event Action JumpEvent;
	float Horizontal { get; }
	void SwitchControl(bool switcher);
}
