//using System;

//namespace MyParser
//{
//    public delegate VauleExpression ExternalLookupHandler(string name);
//    public partial class ExpressionParser
//    {
//        private ExternalLookupHandler externalLookupMethod;

//        public void SetExternalLookupMethod(ExternalLookupHandler method)
//        {
//            if (method == null)
//                throw new ArgumentNullException();

//            externalLookupMethod = method;
//        }

//        public VauleExpression ExternalLookup(string name)
//        {
//            if (externalLookupMethod != null)
//                return externalLookupMethod(name);

//            return new VauleExpression.IntVauleExpression(0); //default
//        }
//    }
//}
