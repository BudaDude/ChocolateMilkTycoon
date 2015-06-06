using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class ReportView : MonoBehaviour {

    //Profit
    public Text cupsSoldText;
    public Text avgPriceText;
    public Text otherIncomeText;

    //Expenses
    public Text expensesText;
    public Text milkExpenseText;
    public Text cocoaExpenseText;
    public Text sugarExpenseText;
    public Text rentExpenseText;
    public Text otherExpenseText;


    //bottom
    public Text profitText;
    public Text newBalanceText;



    private GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.FindChild("Graphics").transform)
        {


        cupsSoldText.text = gm.sales.ToString();
        if (gm.sales > 0)
        {
            avgPriceText.text = gm.salesPriceHistory.Average().ToString("C2");
        }

        expensesText.text = gm.moneySpent.ToString("C2");
        cocoaExpenseText.text = gm.cocoaExpense.ToString("C2");
        sugarExpenseText.text= gm.sugarExpense.ToString("C2");
        milkExpenseText.text = gm.milkExpense.ToString("C2");
        rentExpenseText.text = gm.currentLocation.rent.ToString("C2");

        profitText.text = gm.moneyEarned.ToString("C2");
        newBalanceText.text = gm.money.ToString("C2");


        }


	}
}
