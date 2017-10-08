using System.Reflection;

namespace Our.Umbraco.Webhooks4Umbraco.Events
{
    internal sealed class EventProxy
    {
        public delegate void OnEventHandler(object target, string eventName, object[] parameters);

        private readonly OnEventHandler _handler;
        private readonly object _target;
        private readonly string _name;

        public EventProxy(OnEventHandler handler, object target, string name)
        {
            this._handler = handler;
            this._target = target;
            this._name = name;
        }

        #region Event handlers

        public void OnEvent0()
        {
            if (this._handler != null)
            {
                var parameters = new object[0];
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent1(object p1)
        {
            if (this._handler != null)
            {
                var parameters = new object[1];
                parameters[0] = p1;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent2(object p1, object p2)
        {
            if (this._handler != null)
            {
                var parameters = new object[2];
                parameters[0] = p1;
                parameters[1] = p2;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent3(object p1, object p2, object p3)
        {
            if (this._handler != null)
            {
                var parameters = new object[3];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent4(object p1, object p2, object p3, object p4)
        {
            if (this._handler != null)
            {
                var parameters = new object[4];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent5(object p1, object p2, object p3, object p4, object p5)
        {
            if (this._handler != null)
            {
                var parameters = new object[5];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent6(object p1, object p2, object p3, object p4, object p5, object p6)
        {
            if (this._handler != null)
            {
                var parameters = new object[6];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                parameters[5] = p6;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent7(object p1, object p2, object p3, object p4, object p5, object p6, object p7)
        {
            if (this._handler != null)
            {
                var parameters = new object[7];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                parameters[5] = p6;
                parameters[6] = p7;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent8(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
        {
            if (this._handler != null)
            {
                var parameters = new object[8];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                parameters[5] = p6;
                parameters[6] = p7;
                parameters[7] = p8;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent9(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9)
        {
            if (this._handler != null)
            {
                var parameters = new object[9];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                parameters[5] = p6;
                parameters[6] = p7;
                parameters[7] = p8;
                parameters[8] = p9;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        public void OnEvent10(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)
        {
            if (this._handler != null)
            {
                var parameters = new object[10];
                parameters[0] = p1;
                parameters[1] = p2;
                parameters[2] = p3;
                parameters[3] = p4;
                parameters[4] = p5;
                parameters[5] = p6;
                parameters[6] = p7;
                parameters[7] = p8;
                parameters[8] = p9;
                parameters[9] = p10;
                this._handler.Invoke(this._target, this._name, parameters);
            }
        }

        #endregion

        /// <summary>
        /// The MethodInfos of the events raised in this class.
        /// </summary>
        public static MethodInfo[] OnEventMethodInfos = null;

        static EventProxy()
        {
            OnEventMethodInfos = new MethodInfo[11];

            for (int i = 0; i <= 10; i++)
            {
                OnEventMethodInfos[i] = typeof(EventProxy).GetMethod("OnEvent" + i);
            }
        }
    }
}