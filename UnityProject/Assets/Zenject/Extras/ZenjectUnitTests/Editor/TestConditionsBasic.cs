using System;
using System.Collections.Generic;
using Zenject;
using NUnit.Framework;
using System.Linq;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests
{
    [TestFixture]
    public class TestConditionsBasic : TestWithContainer
    {
        public interface IFoo
        {
        }

        class Foo1 : IFoo
        {
        }

        class Foo2 : IFoo
        {
        }

        class Bar1
        {
            public IFoo Foo;

            public Bar1(IFoo foo)
            {
                Foo = foo;
            }
        }

        class Bar2
        {
            public IFoo Foo;

            public Bar2(IFoo foo)
            {
                Foo = foo;
            }
        }

        [Test]
        public void Test1()
        {
            Container.Bind<Bar1>().ToSingle();
            Container.Bind<Bar2>().ToSingle();
            Container.Bind<IFoo>().ToSingle<Foo1>();
            Container.Bind<IFoo>().ToSingle<Foo2>().WhenInjectedInto<Bar2>();

            Assert.IsNotEqual(
                Container.Resolve<Bar1>().Foo, Container.Resolve<Bar2>().Foo);
        }
    }
}



