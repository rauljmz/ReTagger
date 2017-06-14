using ReTagger.StateMachine;
using System;
using Xunit;

namespace ReTaggerTests.Machine
{
    public class MachineTests
    {
        [Fact]
        public void StatesAreAdded()
        {
            var A = new State<string>(1, null, null, null);
            var B = new State<string>(2, null, null, null);
            var machine = new Machine<string>()
                .AddState(A)
                .AddState(B);
            Assert.Collection(machine.States,
                e => e.Equals(A),
                e => e.Equals(B)
                );
        }

        [Fact]
        public void CannotAddStatesSameID()
        {
            var A = new State<string>(1, null, null, null);
            var B = new State<string>(1, null, null, null);
            Assert.Throws<Exception>(() => new Machine<string>().AddState(A).AddState(B));            
        }

        [Fact]
        public void NextExectuesAction()
        {
            bool executed = false;
            var A = new State<object>(1, _ => true, _ => executed = true, new object [] { 1 });
            var machine = new Machine<object>().AddState(A);
            machine.Next(null);
            Assert.True(executed);
        }

        [Fact]
        public void NextDoesExectuesActionIfGuardFails()
        {
            bool executed = false;
            var A = new State<object>(1, _ => false, _ => executed = true, new object[] { 1 });
            var machine = new Machine<object>().AddState(A);
            machine.Next(null);
            Assert.False(executed);
        }

        [Fact]
        public void NextMovesToNextState()
        {
            bool executed = false;
            var A = new State<object>(1, _ => false , null, new object[] { 2 });
            var B = new State<object>(2, _ => true , _ => executed = true, new object[] { 2 });
            var machine = new Machine<object>().AddState(A).AddState(B);
            machine.Next(null);
            Assert.True(executed);
        }

        [Fact]
        public void IsValidStateTest()
        {
            var A = new State<object>(1, _ => false, null, new object[] { 2 });
            var B = new State<object>(2, _ => true, null, new object[] { 2 });
            var machine = new Machine<object>().AddState(A, false).AddState(B, true);
            machine.Next(null);
            Assert.True(machine.IsInValidFinalState());
        }

    }
}
