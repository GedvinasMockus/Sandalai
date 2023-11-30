using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class AnimationFactory
    {
        public bool UseCache { get; set; }

        private Dictionary<string, AnimationShared> sharedStorage;
        public AnimationFactory()
        {
            sharedStorage = new Dictionary<string, AnimationShared>();
            UseCache = true;
        }
        public Animation CreateAnimation(string name, ContentManager content, float scale, float frameDuration, SpriteEffects flip)
        {
            if (UseCache && sharedStorage.ContainsKey(name))
            {
                AnimationShared cached = sharedStorage[name];
                Animation a;
                if (cached is RangedAttackAnimationShared)
                    a = new RangedAttackAnimation((cached as RangedAttackAnimationShared), frameDuration, flip);
                else
                    a = new Animation(cached, frameDuration, flip);
                a.Scale = scale;
                return a;
            }
            switch (name)
            {
                case "KunoichiIdle":
                    AnimationShared KunoichiIdleShared = new AnimationShared(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 9, true, 14, 64, new Vector2(53, 64));
                    if(UseCache) sharedStorage.Add(name, KunoichiIdleShared);
                    Animation KunoichiIdleAnimation = new Animation(KunoichiIdleShared, frameDuration, flip);
                    KunoichiIdleAnimation.Scale = scale;
                    return KunoichiIdleAnimation;
                case "KunoichiJump":
                    AnimationShared KunoichiJumpShared = new AnimationShared(content.Load<Texture2D>("Character/Ninja/Kunoichi/Jump"), 10, false, 14, 64, new Vector2(53, 64));
                    if(UseCache) sharedStorage.Add(name, KunoichiJumpShared);
                    Animation KunoichiJumpAnimation = new Animation(KunoichiJumpShared, frameDuration, flip);
                    KunoichiJumpAnimation.Scale = scale;
                    return KunoichiJumpAnimation;
                case "KunoichiRun":
                    AnimationShared KunoichiRunShared = new AnimationShared(content.Load<Texture2D>("Character/Ninja/Kunoichi/Run"), 8, false, 14, 64, new Vector2(53, 64));
                    if(UseCache) sharedStorage.Add(name, KunoichiRunShared);
                    Animation KunoichiRunAnimation = new Animation(KunoichiRunShared, frameDuration, flip);
                    KunoichiRunAnimation.Scale = scale;
                    return KunoichiRunAnimation;
                case "SamuraiIdle":
                    AnimationShared SamuraiIdleShared = new AnimationShared(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 5, true, 16, 68, new Vector2(51, 60));
                    if(UseCache) sharedStorage.Add(name, SamuraiIdleShared);
                    Animation SamuraiIdleAnimation = new Animation(SamuraiIdleShared, frameDuration, flip);
                    SamuraiIdleAnimation.Scale = scale;
                    return SamuraiIdleAnimation;
                case "SamuraiJump":
                    AnimationShared SamuraiJumpShared = new AnimationShared(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Jump"), 7, false, 16, 68, new Vector2(51, 60));
                    if(UseCache) sharedStorage.Add(name, SamuraiJumpShared);
                    Animation SamuraiJumpAnimation = new Animation(SamuraiJumpShared, frameDuration, flip);
                    SamuraiJumpAnimation.Scale = scale;
                    return SamuraiJumpAnimation;
                case "SamuraiRun":
                    AnimationShared SamuraiRunShared = new AnimationShared(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run"), 8, false, 16, 68, new Vector2(51, 60));
                    if(UseCache) sharedStorage.Add(name, SamuraiRunShared);
                    Animation SamuraiRunAnimation = new Animation(SamuraiRunShared, frameDuration, flip);
                    SamuraiRunAnimation.Scale = scale;
                    return SamuraiRunAnimation;
                case "SkeletonIdle":
                    AnimationShared SkeletonIdleShared = new AnimationShared(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Idle"), 7, true, 14, 64, new Vector2(52, 64));
                    if(UseCache) sharedStorage.Add(name, SkeletonIdleShared);
                    Animation SkeletonIdleAnimation = new Animation(SkeletonIdleShared, frameDuration, flip);
                    SkeletonIdleAnimation.Scale = scale;
                    return SkeletonIdleAnimation;
                case "SkeletonWalk":
                    AnimationShared SkeletonWalkShared = new AnimationShared(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Walk"), 8, false, 14, 64, new Vector2(52, 64));
                    if(UseCache) sharedStorage.Add(name, SkeletonWalkShared);
                    Animation SkeletonWalkAnimation = new Animation(SkeletonWalkShared, frameDuration, flip);
                    SkeletonWalkAnimation.Scale = scale;
                    return SkeletonWalkAnimation;
                case "SkeletonShoot":
                    RangedAttackAnimationShared SkeletonShootShared = new RangedAttackAnimationShared(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Shot_1"), 15, false, 14, 64, new Vector2(52, 64), 37, 12, new Vector2(71, 76));
                    if(UseCache) sharedStorage.Add(name, SkeletonShootShared);
                    RangedAttackAnimation SkeletonShootAnimation = new RangedAttackAnimation(SkeletonShootShared, frameDuration, flip);
                    SkeletonShootAnimation.Scale = scale;
                    return SkeletonShootAnimation;
                default:
                    return null;
            }
        }
    }
}
