using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Text pricePot;
    public Text pricePitcher;
    public Text priceInsect;
    public Text priceQty;
    public Text priceAcid;

    public int currentPricePot = 1000;
    public int currentPricePitcher = 500;
    public int currentPriceInsect = 100;
    public int currentPriceQty = 50;
    public int currentPriceAcid = 100;
    public long currentPriceUnknown = 1000000000000;


    int qtyPot = 1;
    int qtyPitcher = 1;
    int qtyQty = 1;
    int qtyAcid = 1;

    public Text txtPot;
    public Text txtPitcher;
    public Text txtInsect;
    public Text txtQty;
    public Text txtAcid;

    public GameObject warning;
    public GameObject warning2;
    public GameObject achivement;
    public GameObject blockPanel;

    string[] insects = { "Bee", "Mosquito", "Beetle" };
    int insectIndex = 0;
    
    int maxPots = 4;
    int maxPitchers = 4;
    int maxInsects = 10;
    int maxAcid = 5;

    public Text pointsTxt;
    int points;
    public GameObject gameMaster;


    public GameObject potAdm;

    public GameObject lane; 

    public GameObject plant;
    float t = 0;

    public void BuyPot()
    {
        if(currentPricePot <= points)
        {
            if (qtyPot < maxPots)
            {
                CheckOut(currentPricePot);
                qtyPot++;
                potAdm.GetComponent<PotManager>().UnlockPot();
                currentPricePot *= 2;

                pricePot.text = "Price: " + currentPricePot;
                txtPot.text = qtyPot.ToString();
            }
            else
            {
                txtPot.text = "MAX";
                pricePot.text = "Price: MAX";
            }
        }
        else
        {
            SendWarning(1);
        }
        if (qtyPot == 1) blockPanel.SetActive(false);
    }

    public void BuyPitcher()
    {
        if (currentPricePitcher <= points)
        {
            if (qtyPitcher < qtyPot)
            {
                if (qtyPitcher < maxPitchers)
                {
                    CheckOut(currentPricePitcher);
                    qtyPitcher++;
                    potAdm.GetComponent<PotManager>().CreatePlant();
                    currentPricePitcher *= 5;

                    pricePitcher.text = "Price: " + currentPricePitcher;
                    txtPitcher.text = qtyPitcher.ToString();
                }
                else
                {
                    txtPitcher.text = "MAX";
                    pricePitcher.text = "Price: MAX";
                }
            }
            else
            {
                SendWarning(2);
            }

        }
        else
        {
            SendWarning(1);
        }
    }

    public void BuyNewInsect()
    {
        if (currentPriceInsect <= points)
        {
            if (insectIndex + 1 <= insects.Length)
            {
                CheckOut(currentPriceInsect);
                insectIndex++;
                Debug.Log("insect index: " + insectIndex);
                lane.GetComponent<LaneScript>().insectsUnlocked++;
                currentPriceInsect *= 2;

                priceInsect.text = "Price: " + currentPriceInsect;
                if (insectIndex < insects.Length)
                    txtInsect.text = insects[insectIndex];
                else
                {
                    txtInsect.text = "MAX";
                    priceInsect.text = "Price: MAX";
                }
            }
        }
        else
        {
                SendWarning(1);
        }
    }

    public void BuyMoreInsects()
    {
        if (currentPriceQty <= points)
        {
            if (qtyQty < maxInsects)
            {
                CheckOut(currentPriceQty);
                qtyQty++;
                lane.GetComponent<LaneScript>().quantityUnlocked++;
                currentPriceQty *= 2;

                priceQty.text = "Price: " + currentPriceQty;
                txtQty.text = qtyQty.ToString();
            }
            else
            {
                txtQty.text = "MAX";
                priceQty.text = "Price: MAX";
            }
        }
        else
        {
            SendWarning(1);
        }
    }

    public void BuyAcid()
    {
        if (currentPriceAcid <= points)
        {
            if(qtyAcid < maxAcid)
            {
                CheckOut(currentPriceAcid);
                qtyAcid++;
                plant.GetComponent<PlantAdmin>().plantAddedTime--;
                currentPriceAcid *= 2;

                priceAcid.text = "Price: " + currentPriceAcid;
                txtAcid.text = qtyAcid.ToString();
            }
            else
            {
                txtAcid.text = "MAX";
                priceAcid.text = "Price: MAX";
            }
        }
        else
        {
            SendWarning(1);
        }
    }

    public void BuyUnknown()
    {
        if (currentPriceUnknown <= points)
        {
            SendAchivement();
        }
        else
        {
            SendWarning(1);
        }
    }

    void SendAchivement()
    {
        achivement.SetActive(true);
    }

    void SendWarning(int number)
    {
        if(number == 1)
        {
            warning.SetActive(true);
        }
        else if(number == 2)
        {
            warning2.SetActive(true);
        }
        
    }

    void CheckOut(int cost)
    {
        plant.GetComponent<PlantAdmin>().totalPoints -= cost;
    }
    void Start()
    {
        points = plant.GetComponent<PlantAdmin>().totalPoints;
    }

    void Update()
    {
        points = plant.GetComponent<PlantAdmin>().totalPoints;

        if (warning.activeSelf || achivement.activeSelf || warning2.activeSelf)
        {
            t += Time.unscaledDeltaTime;
            if (t > 1.5f) {
                warning.SetActive(false);
                achivement.SetActive(false);
                warning2.SetActive(false);
                t = 0f;
            } 
        }
    }
}
