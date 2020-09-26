using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int column, row, targetX, targetY, previousColumn, previousRow;
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
        previousRow = row;
        previousColumn = column;
    }
    // Update is called once per frame
    void Update()
    { 
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
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

        if (Mathf.Abs(targetY - transform.position.y) > .1)
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

        StartCoroutine(SwipeEmptyTile());

        transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.x - transform.position.y);

        FindMatches();
        if (isMatched)
        {
            board.DestroyMatches();
        }
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
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right swipe 
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column--;
            column++;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up swipe 
            GameObject upDot1 = board.allDots[column, row + 1];
            if (upDot1.tag != "Empty")
            {
                otherDot = board.allDots[column, row + 1];
                otherDot.GetComponent<Dot>().row--;
                row++;
            }
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Left swipe 
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column++;
            column--;
        }
        else if ((swipeAngle < -45 && swipeAngle >= -135 && row > 0))
        {
            //Down swipe 
            otherDot = board.allDots[column, row - 1];
            otherDot.GetComponent<Dot>().row++;
            row--;
        }
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot = board.allDots[column - 1, row];
            GameObject rightDot = board.allDots[column + 1, row];
            if (leftDot != null && rightDot != null)
            {
                if (leftDot.tag == this.gameObject.tag && rightDot.tag == this.gameObject.tag && this.gameObject.tag != "Empty")
                {
                    leftDot.GetComponent<Dot>().isMatched = true;
                    rightDot.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot = board.allDots[column, row + 1];
            GameObject downDot = board.allDots[column, row - 1];
            if (upDot != null && downDot != null)
            {
                if (upDot.tag == this.gameObject.tag && downDot.tag == this.gameObject.tag && this.gameObject.tag != "Empty")
                {
                    upDot.GetComponent<Dot>().isMatched = true;
                    downDot.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

    private IEnumerator SwipeEmptyTile()
    {
        if (row > 0)
        {
            GameObject downDot = board.allDots[column, row - 1];
            if (downDot.tag == "Empty")
                row--;
        }
        yield return new WaitForSeconds(.4f);
    }
}
