namespace HalloLinq
{
    delegate void EinfacherDelegate(); //Action
    delegate void DelegateMitPara(string msg); //Action<string>
    delegate long CalcDelegate(int x, int y); //Func<int, int, long> 

    internal class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            EinfacherDelegate meinDeleAno = delegate () { Console.WriteLine("Hallo"); };
            Action meinDeleAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAlsAction = MethodeMitPara;
            DelegateMitPara deleMitParaAno = delegate (string msg) { Console.WriteLine("Hallo " + msg); };
            DelegateMitPara deleMitParaAno2 = (string msg) => { Console.WriteLine("Hallo " + msg); };
            DelegateMitPara deleMitParaAno3 = (msg) => Console.WriteLine("Hallo " + msg);
            DelegateMitPara deleMitParaAno4 = x => Console.WriteLine("Hallo " + x);

            CalcDelegate calcDele = Multiply;
            Func<int, int, long> calcDeleAlsFunc = Sum;
            CalcDelegate calcDeleAno = delegate (int a, int b) { return a + b; };
            CalcDelegate calcDeleAno2 = (int a, int b) => { return a + b; };
            CalcDelegate calcDeleAno3 = (a, b) => { return a + b; };
            CalcDelegate calcDeleAno4 = (a, b) => a + b;

            long result = calcDele.Invoke(2, 3);

            //var personen = new List<Person>();
            //personen.Where(FilterP);
            var texte = new List<string>();
            texte.Where(Filter);
            texte.Where(x => x.StartsWith("b"));
            texte.Where(x =>
            {
                if (x.StartsWith("b"))
                    return true;
                else
                    return false;
            });
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Multiply(int x, int y)
        {
            return x * y;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string name)
        {
            Console.WriteLine("Hallo " + name);
        }

        public void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
