using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using game_forest_test.Models;

namespace game_forest_test.Views;

public class PlayboardAnimator
{
    public void SwapAnimation(Vector2 sourcePosition, Vector2 targetPosition, PlayboardView view, int duration)
    {
        var sourceElement = view.Data[sourcePosition];
        var targetElement = view.Data[targetPosition];
    
        var sourceLeft = Canvas.GetLeft(sourceElement);
        var sourceTop = Canvas.GetTop(sourceElement);
    
        var targetLeft = Canvas.GetLeft(targetElement);
        var targetTop = Canvas.GetTop(targetElement);
    
        DoubleAnimation sourceElementAnimationX = new DoubleAnimation();
        DoubleAnimation sourceElementAnimationY = new DoubleAnimation();
        
        DoubleAnimation targetElementAnimationX = new DoubleAnimation();
        DoubleAnimation targetElementAnimationY = new DoubleAnimation();

        sourceElementAnimationX.From = sourceLeft;
        sourceElementAnimationY.From = sourceTop;
        sourceElementAnimationX.To = targetLeft;
        sourceElementAnimationY.To = targetTop;
        sourceElementAnimationX.Duration = TimeSpan.FromMilliseconds(duration);
        sourceElementAnimationY.Duration = TimeSpan.FromMilliseconds(duration);
        
        targetElementAnimationX.From = targetLeft;
        targetElementAnimationY.From = targetTop;
        targetElementAnimationX.To = sourceLeft;
        targetElementAnimationY.To = sourceTop;
        targetElementAnimationX.Duration = TimeSpan.FromMilliseconds(duration);
        targetElementAnimationY.Duration = TimeSpan.FromMilliseconds(duration);
        
        sourceElement.BeginAnimation(Canvas.LeftProperty, sourceElementAnimationX);
        sourceElement.BeginAnimation(Canvas.TopProperty, sourceElementAnimationY);
        
        targetElement.BeginAnimation(Canvas.LeftProperty, targetElementAnimationX);
        targetElement.BeginAnimation(Canvas.TopProperty, targetElementAnimationY);
    }

    public void GenerationAnimation(Vector2 sourcePosition, Vector2 targetPosition, PlayboardView view, int duration)
    {
        
        var sourceElement = view.Data[sourcePosition];
        var targetElement = view.Data[targetPosition];
    
        var sourceLeft = Canvas.GetLeft(sourceElement);
        var sourceTop = Canvas.GetTop(sourceElement);
    
        var targetLeft = Canvas.GetLeft(targetElement);
        var targetTop = Canvas.GetTop(targetElement);
        
        Canvas.SetLeft(targetElement, sourceLeft);
        Canvas.SetTop(targetElement, sourceTop);
        
        Canvas.SetZIndex(targetElement,  10);
        
        DoubleAnimation targetElementAnimationX = new DoubleAnimation();
        DoubleAnimation targetElementAnimationY = new DoubleAnimation();
        
        targetElementAnimationX.From = sourceLeft;
        targetElementAnimationY.From = sourceTop;
        targetElementAnimationX.To = targetLeft;
        targetElementAnimationY.To = targetTop;
        targetElementAnimationX.Duration = TimeSpan.FromMilliseconds(duration);
        targetElementAnimationY.Duration = TimeSpan.FromMilliseconds(duration);
        
        targetElement.BeginAnimation(Canvas.LeftProperty, targetElementAnimationX);
        targetElement.BeginAnimation(Canvas.TopProperty, targetElementAnimationY);
    }
}