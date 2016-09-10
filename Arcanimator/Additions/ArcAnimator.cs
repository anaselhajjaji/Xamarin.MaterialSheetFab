using System;
using Android.Runtime;

namespace IO.Codetail.Animation.Arcanimator
{
    public partial class ArcAnimator
    {
        static IntPtr id_setStartDelay_J;
        static IntPtr id_getStartDelay;

        public override unsafe long StartDelay {
            // Metadata.xml XPath method reference: path="/api/package[@name='io.codetail.animation.arcanimator']/class[@name='ArcAnimator']/method[@name='getStartDelay' and count(parameter)=0]"
            [Register ("getStartDelay", "()J", "GetGetStartDelayHandler")]
            get {
                if (id_getStartDelay == IntPtr.Zero)
                    id_getStartDelay = JNIEnv.GetMethodID (class_ref, "getStartDelay", "()J");
                try {

                    if (GetType () == ThresholdType)
                        return JNIEnv.CallLongMethod  (Handle, id_getStartDelay);
                    else
                        return JNIEnv.CallNonvirtualLongMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getStartDelay", "()J"));
                } finally {
                }
            }

            [Register ("setStartDelay", "(J)V", "GetSetStartDelay_JHandler")]
            set {
                if (id_setStartDelay_J == IntPtr.Zero)
                    id_setStartDelay_J = JNIEnv.GetMethodID (class_ref, "setStartDelay", "(J)V");
                try {
                    JValue* __args = stackalloc JValue [1];
                    __args [0] = new JValue (value);

                    if (GetType () == ThresholdType)
                        JNIEnv.CallVoidMethod  (Handle, id_setStartDelay_J, __args);
                    else
                        JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setStartDelay", "(J)V"), __args);
                } finally {
                }
            }
        }
    }
}

