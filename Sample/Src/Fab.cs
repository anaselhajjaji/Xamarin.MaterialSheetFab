using System;
using Android.Support.Design.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Graphics;

using Com.Gordonwong.Materialsheetfab;

namespace Sample
{
    public class Fab : FloatingActionButton, IAnimatedFab {

        private static int FAB_ANIM_DURATION = 200;

        public Fab(Context context) : base(context) {
        }

        public Fab(Context context, IAttributeSet attrs) : base(context, attrs) {
        }

        public Fab(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) {
        }

        public override void Show() {
            Show(0, 0);
        }

        public void Show(float translationX, float translationY) {
            // Set FAB's translation
            SetTranslation(translationX, translationY);

            // Only use scale animation if FAB is hidden
            if (Visibility != ViewStates.Visible) {
                // Pivots indicate where the animation begins from
                float pivotX = PivotX + translationX;
                float pivotY = PivotY + translationY;

                ScaleAnimation anim;
                // If pivots are 0, that means the FAB hasn't been drawn yet so just use the
                // center of the FAB
                if (pivotX == 0 || pivotY == 0) {
                    anim = new ScaleAnimation(0, 1, 0, 1, Dimension.RelativeToSelf, 0.5f,
                        Dimension.RelativeToSelf, 0.5f);
                } else {
                    anim = new ScaleAnimation(0, 1, 0, 1, pivotX, pivotY);
                }

                // Animate FAB expanding
                anim.Duration = FAB_ANIM_DURATION;
                anim.Interpolator = GetInterpolator();
                StartAnimation(anim);
            }
            Visibility = ViewStates.Visible;
        }

        public override void Hide() {
            // Only use scale animation if FAB is visible
            if (Visibility == ViewStates.Visible) {
                // Pivots indicate where the animation begins from
                float pivotX = PivotX + TranslationX;
                float pivotY = PivotY + TranslationY;

                // Animate FAB shrinking
                ScaleAnimation anim = new ScaleAnimation(1, 0, 1, 0, pivotX, pivotY);
                anim.Duration = FAB_ANIM_DURATION;
                anim.Interpolator = GetInterpolator();
                StartAnimation(anim);
            }
            Visibility = ViewStates.Invisible;
        }

        private void SetTranslation(float translationX, float translationY) {
            Animate().SetInterpolator(GetInterpolator()).SetDuration(FAB_ANIM_DURATION)
                .TranslationX(translationX).TranslationY(translationY);
        }

        private Android.Views.Animations.IInterpolator GetInterpolator() {
            return AnimationUtils.LoadInterpolator(Context, Resource.Interpolator.msf_interpolator);
        }
    }

}

