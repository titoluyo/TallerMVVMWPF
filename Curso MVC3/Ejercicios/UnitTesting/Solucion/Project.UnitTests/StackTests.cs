namespace Project.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Project;

    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void IsEmpty_NuevoStack_RetornaVerdadero()
        {
            Stack stack=new Stack();

            bool estaVacio = stack.IsEmpty;

            Assert.IsTrue(estaVacio);
        }

        [TestMethod]
        public void IsEmpty_IngresoUnElemento_RetornaFalso()
        {
            Stack stack=new Stack();
            stack.Push(1);

            bool estaVacio = stack.IsEmpty;

            Assert.IsFalse(estaVacio);
        }

        [TestMethod]
        public void IsEmpty_IngresoYSacoUnElemento_RetornaFalso()
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Pop();

            bool estaVacio = stack.IsEmpty;

            Assert.IsTrue(estaVacio);
        }

        [TestMethod]
        public void IsEmpty_IngresoDosObtengoUno_RetornaFalso()
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Pop();

            bool isEmpty = stack.IsEmpty;

            Assert.IsFalse(isEmpty);
        }

        [TestMethod]
        public void Pop_IngresoYObtengoUnElemento_ElElementoEsElMismo()
        {
            Stack stack = new Stack();
            stack.Push(1);
            
            int element=stack.Pop();

            Assert.AreEqual(1, element);
        }

        [TestMethod]
        public void Pop_IngresoDosElementosYObtengoUno_ElElementoObtenidoEsIgualAlPrimero()
        {
            Stack stack=new Stack();
            stack.Push(1);
            stack.Push(2);

            int element = stack.Pop();

            Assert.AreEqual(2, element);
        }

        [TestMethod]
        public void Pop_IngresoDosElementosYObtengoDos_ElSegundoObtenidoEsIgualAlPrimerIngresado()
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Pop();

            int element = stack.Pop();

            Assert.AreEqual(1, element);
        }


    }
}
