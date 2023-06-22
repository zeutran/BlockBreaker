using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Brick : MonoBehaviour
{
    [FormerlySerializedAs("Laser")] public AudioClip laser;
    public Sprite[] hitSprites;
    public int brickHealth;
    public GameObject smoke;
    public SpriteRenderer icon;
    public BrickData brickData;
    public Vector2 brickPos;
    public int specialBrickCount;
    public int specialBrickDamage = 750;
    private int _destroyCombine;
    private bool _isBreakable; // TODO: Make this a property for the future
    public int horizontalDamageCount;
    public int verticalDamageCount;
    public BrickData.BrickType brickType;
    
    void Start()
    {
        _isBreakable = (this.tag == "Breakable");
    }
    public void InitBrick(BrickData data, Vector2 pos)
    {
        brickData = data;
        brickHealth = brickData.brickHealth;
        brickType = brickData.brickType;
        brickPos = pos;

        switch (brickType)
        {
            case BrickData.BrickType.A:
                specialBrickCount = horizontalDamageCount;
                icon.color = Color.red;
                break;
            case BrickData.BrickType.B:
                specialBrickCount = verticalDamageCount;
                icon.color = Color.blue;
                break;
            case BrickData.BrickType.C:
                specialBrickCount = 8;
                icon.color = Color.green;
                break;
            case BrickData.BrickType.D:
                icon.color = Color.yellow;
                break;
        }
        ShowBrick();
    }

    private void ShowBrick()
    {
        float duration = (brickPos.y + 25) / 25;
        gameObject.transform.localPosition = new Vector3(brickPos.x, brickPos.y + 30f, 0);
        gameObject.transform.DOLocalMove(new Vector3(brickPos.x, brickPos.y, 0),
                duration)
            .SetEase(Ease.OutBack, .3f).SetDelay(.03f).OnComplete(() =>
            {
                Ball.instance.levelStarted = true;
                DOTween.Kill(gameObject.transform);
            });
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        AudioSource.PlayClipAtPoint(laser, transform.position);
        if (_isBreakable)
        {
            HandleHits(LevelManager.instance.currentBallDamage);
            LevelManager.instance.ScoreAddition(LevelManager.instance.currentBallDamage);
        }
    }

    private void ShakeAnimation()
    {
        DOTween.Kill(gameObject.transform);
        transform.DOShakePosition(.2f, .1f, 30, 90f, false, true);
    }

    public void SpecialDamage()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem.MainModule main = smokePuff.GetComponent<ParticleSystem>().main;
        main.startColor = Color.white;
    }
    public void HandleHits(int damage)
    {
        brickHealth -= damage;
        ShakeAnimation();
        _destroyCombine = 0;
        
        if (brickHealth <= 0)
        {
            LevelManager.instance.currentBricks.Remove(this);
            switch (brickType)
            {
                case BrickData.BrickType.A:
                    List<Brick> tempEffectListA = new List<Brick>();
                    List<Brick> tempCurrentAllListA = new List<Brick>(LevelManager.instance.currentBricks);
                    foreach (Brick brick in tempCurrentAllListA)
                    {
                        if(Math.Abs(brick.brickPos.x - this.brickPos.x) < .1f)
                            tempEffectListA.Add(brick);
                        if(tempEffectListA.Count == specialBrickCount)
                            break;
                    }
                    foreach (Brick brick in tempEffectListA)
                    {
                        int brickHealthTemp = brick.brickHealth;
                        if (brickHealthTemp- specialBrickDamage <= 0)
                        {
                            _destroyCombine++;
                        }
                        brick.SpecialDamage();
                        brick.HandleHits(specialBrickDamage);
                    }
                    Debug.Log("A special brick destroyed " + _destroyCombine + " bricks" + " with " + tempEffectListA.Count + " alistcount");
                    if(_destroyCombine > 0) 
                        LevelManager.instance.ScoreAddition(specialBrickDamage * _destroyCombine);
                    Debug.Log("Score added combined " + specialBrickDamage * _destroyCombine + " points");

                    break;
                case BrickData.BrickType.B:
                    List<Brick> tempEffectListB = new List<Brick>();
                    List<Brick> tempCurrentAllListB = new List<Brick>(LevelManager.instance.currentBricks);
                    foreach (Brick brick in tempCurrentAllListB)
                    {
                        if(Math.Abs(brick.brickPos.y - this.brickPos.y) < .1f)
                            tempEffectListB.Add(brick);
                        if(tempEffectListB.Count == specialBrickCount)
                            break;
                    }
                    foreach (Brick brick in tempEffectListB)
                    {
                        int brickHealthTemp = brick.brickHealth;
                        if (brickHealthTemp - specialBrickDamage <= 0)
                        {
                            _destroyCombine++;
                        }
                        brick.SpecialDamage();
                        brick.HandleHits(specialBrickDamage);
                    }
                    Debug.Log("B special brick destroyed " + _destroyCombine + " bricks" + " with " + tempEffectListB.Count + " alistcount");

                    if(_destroyCombine > 0) 
                        LevelManager.instance.ScoreAddition(specialBrickDamage * _destroyCombine);
                    Debug.Log("Score added combined " + specialBrickDamage * _destroyCombine + " points");
                    break;
                case BrickData.BrickType.C:
                    List<Brick> tempCurrentAllListC = new List<Brick>(LevelManager.instance.currentBricks);
                    List<Brick> tempEffectListC = new List<Brick>();
                    foreach (Brick brick in tempCurrentAllListC)
                    {
                        tempEffectListC.Add(brick);
                        if(tempEffectListC.Count == specialBrickCount)
                            break;
                    }
                    foreach (Brick brick in tempEffectListC)
                    {
                        int brickHealthTemp = brick.brickHealth;
                        if (brickHealthTemp - specialBrickDamage <= 0)
                        {
                            _destroyCombine++;
                        }
                        brick.SpecialDamage();
                        brick.HandleHits(specialBrickDamage);
                    }
                    Debug.Log("C special brick destroyed " + _destroyCombine + " bricks" + " with " + tempEffectListC.Count + " alistcount");

                    if(_destroyCombine > 0) 
                        LevelManager.instance.ScoreAddition(specialBrickDamage * _destroyCombine);
                    Debug.Log("Score added combined " + specialBrickDamage * _destroyCombine + " points");

                    break;
            }
            LevelManager.instance.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem.MainModule main = smokePuff.GetComponent<ParticleSystem>().main;
        main.startColor = icon.color;
    }

    private int SpriteIndex()
    {
        float value = (float)brickHealth / (float)brickData.brickHealth;
        Debug.Log("Value: " + value);
        if (value >= 1)
            return 0;
        if (value >= 0.75f)
            return 1;
        if (value >= 0.5f)
            return 2;
        if (value >= 0.25f)
            return 3;
        return 4;
    }

    void LoadSprites()
    {
        int spriteIndex = SpriteIndex();
        if (hitSprites[spriteIndex] != null)
            icon.sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Brick Sprite Missing");
    }
}