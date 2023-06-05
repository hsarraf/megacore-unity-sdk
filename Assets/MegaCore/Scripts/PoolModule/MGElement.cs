namespace MegaCore.PoolingModule
{

    public class MGElement: MGElementBase
    {

        public enum Type
        {
            na, // -> when type is not required
            #region List types of elements
            Element1, Element2
            #endregion
        }

        public Type _type;

        public override void Kill(float timeToKill = 0)
        {
            base.Kill(timeToKill);
        }

        public override void ResetElement()
        {
            base.ResetElement();
        }

    }

}