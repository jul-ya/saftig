using System;

public interface IStateVisitor
{
	void Accept(IStateVisitor other);
	void Visit(AttackState attack);
	void Visit(BlockState block);
	void Visit(CrouchState crouch);
	void Visit(IdleState idle);
	void Visit(TypingState typing);
	void Visit(StunState stun);
}

