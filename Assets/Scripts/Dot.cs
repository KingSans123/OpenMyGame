using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int column, row, targetX, targetY;
    public bool isMatched = false;

    private GameObject otherDot;
    private Board board; 
    private Vector2 firstTouchPosition, finalTouchPosition, tempPosition;
    public float swipeAngle = 0;

    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }
    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1f)
        {
            //Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1f)
        {
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.x - transform.position.y);

        FindMatches();
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .1f);
        }
        StartCoroutine(SwipeEmptyTile());
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        MovePieces();
        FindMatches();
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right swipe 
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column--;
            column++;
            board.allDots[column, row] = this.gameObject;
            board.allDots[column - 1, row] = otherDot;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up swipe 
            otherDot = board.allDots[column, row + 1];
            if (otherDot.tag != "Empty")
            {
                otherDot.GetComponent<Dot>().row--;
                row++;
                board.allDots[column, row] = this.gameObject;
                board.allDots[column, row - 1] = otherDot;
            }
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Left swipe 
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column++;
            column--;
            board.allDots[column, row] = this.gameObject;
            board.allDots[column + 1, row] = otherDot;
        }
        else if ((swipeAngle < -45 && swipeAngle >= -135 && row > 0))
        {
            //Down swipe 
            otherDot = board.allDots[column, row - 1];
            otherDot.GetComponent<Dot>().row++;
            row--;
            board.allDots[column, row] = this.gameObject;
            board.allDots[column, row + 1] = otherDot;
        }
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot = board.allDots[column - 1, row];
            GameObject rightDot = board.allDots[column + 1, row];
            if (leftDot.tag == this.gameObject.tag && rightDot.tag == this.gameObject.tag && this.gameObject.tag != "Empty")
            {
                leftDot.GetComponent<Dot>().isMatched = true;
                rightDot.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot = board.allDots[column, row + 1];
            GameObject downDot = board.allDots[column, row - 1];
            if (upDot.tag == this.gameObject.tag && downDot.tag == this.gameObject.tag && this.gameObject.tag != "Empty")
            {
                    upDot.GetComponent<Dot>().isMatched = true;
                    downDot.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
            }
        }
    }

    private IEnumerator SwipeEmptyTile()
    {
        if (row > 0)
        {
            GameObject downDot = board.allDots[column, row - 1];
            if (downDot.tag == "Empty" && this.gameObject.tag != "Empty")
            {
                downDot.GetComponent<Dot>().row++;
                row--;
            }
        }
        yield return new WaitForSecondsRealtime(.4f);
    }
}
