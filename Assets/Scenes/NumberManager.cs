using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberManager : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] Button button4;
    [SerializeField] Button button5;
    [SerializeField] Button button6;
    [SerializeField] Button button7;
    [SerializeField] Button button8;
    [SerializeField] Button button9;
    [SerializeField] Button button10;
    Button[] buttons;
    int[] selectButtons = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int[, ] relasionshipNumbers = {
    //   1  2  3  4  5  6  7  8  9  10
        {0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
        {1, 0, 1, 1, 1, 0, 0, 0, 0, 0},
        {1, 1, 0, 0, 1, 1, 0, 0, 0, 0},
        {0, 1, 0, 0, 1, 0, 1, 1, 0, 0},
        {0, 1, 1, 1, 0, 1, 0, 1, 1, 0},

        {0, 0, 1, 0, 1, 0, 0, 0, 1, 1},
        {0, 0, 0, 1, 0, 0, 0, 1, 0, 0},
        {0, 0, 0, 1, 1, 0, 1, 0, 1, 0},
        {0, 0, 0, 0, 1, 1, 0, 1, 0, 1},
        {0, 0, 0, 0, 0, 1, 0, 0, 1, 0},
    };
    [SerializeField] TextMeshProUGUI buttonText1;
    [SerializeField] TextMeshProUGUI buttonText2;
    [SerializeField] TextMeshProUGUI buttonText3;
    [SerializeField] TextMeshProUGUI buttonText4;
    [SerializeField] TextMeshProUGUI buttonText5;
    [SerializeField] TextMeshProUGUI buttonText6;
    [SerializeField] TextMeshProUGUI buttonText7;
    [SerializeField] TextMeshProUGUI buttonText8;
    [SerializeField] TextMeshProUGUI buttonText9;
    [SerializeField] TextMeshProUGUI buttonText10;
    TextMeshProUGUI[] buttonTexts;
    [SerializeField] Button maxButton;
    [SerializeField] Button minButton;
    [SerializeField] Button sumButton;
    Button[] modeButtons;
    [SerializeField] TextMeshProUGUI moveText;
    int moveNumber = 0;
    [SerializeField] TextMeshProUGUI resultText;
    int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    // mode : max, min, sum
    int[] modeBoolean = {0, 0, 0};
    int modeFlag = 0;
    int[] addNumbers = {0, 1, 4, 5};
    int maxNumber = 0;
    int minNumber = 100;
    int sumNumber = 0;
    [SerializeField] TMP_InputField numberInputField;
    [SerializeField] Button submitButton;
    [SerializeField] TextMeshProUGUI descriptionText;
    int inputNumber = 0;
    int setFlag = 0;
    int randomNumber1 = 0;
    int randomNumber2 = 0;
    int temp = 0;
    int[] inputNumbers = {0, 0, 0, 0,   0, 0, 0, 0,   0, 0};
    int[] candidateNumber = {0, 0, 0, 0,   0, 0, 0, 0,   0, 0};
    int[] countNumbers = {0, 0, 0, 0,   0, 0, 0, 0,   0, 0};
    int selectedCount = 0;
    int inputCount = 0;
    int selectedSumNumber = 0;
    int emptyNumber = 0;
    bool clear = false;

    // Start is called before the first frame update
    void Start() {
        buttons = new Button[] {button1, button2, button3, button4, button5, button6, button7, button8, button9, button10};
        buttonTexts = new TextMeshProUGUI[] {buttonText1, buttonText2, buttonText3, buttonText4, buttonText5, buttonText6, buttonText7, buttonText8, buttonText9, buttonText10};
        modeButtons = new Button[] {maxButton, minButton, sumButton};

        resetnumbers();
        resetNumberInputField();
        resetSubmitButton();

        solvePuzzle();
    }

    // Update is called once per frame
    void Update() {
    }

    public void resetnumbers() {
        for (int i = 0; i < 20; i++) {
            // output 0 ~ numbers.length - 1
            randomNumber1 = UnityEngine.Random.Range(0, numbers.Length);
            // output 0 ~ numbers.length - 1
            randomNumber2 = UnityEngine.Random.Range(0, numbers.Length);

            temp = numbers[randomNumber1];
            numbers[randomNumber1] = numbers[randomNumber2];
            numbers[randomNumber2] = temp;
        }
    }

    public void resetNumberButton() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            selectButtons[i] = 0;
        }
    }

    public void resetModeButton() {
        for (int i = 0; i < modeBoolean.Length; i++) {
            modeBoolean[i] = 0;
            modeButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void resetNumberInputField() {
        numberInputField.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
    }

    public void resetSubmitButton() {
        submitButton.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
    }

    public void clickModeButton(int modeNumber) {
        if (modeBoolean[modeNumber] == 0) {
            resetNumberButton();
            resetModeButton();
            resetNumberInputField();
            resetSubmitButton();

            modeBoolean[modeNumber] = 1;
            modeButtons[modeNumber].GetComponent<Image>().color = new Color32(255, 100, 100, 255);
        } else {
            resetNumberButton();
            resetModeButton();
            resetNumberInputField();
            resetSubmitButton();
        }
    }

    public void clickNumberButton(int buttonNumber) {
        resetNumberInputField();
        resetSubmitButton();

        modeFlag = 0;
        for (int i = 0; i < modeBoolean.Length; i++) {
            if (modeBoolean[i] == 1) {
                modeFlag = 1;
                selectNumber(buttonNumber);
            }
        }

        if (modeFlag == 0) {
            resetNumberButton();

            selectButtons[buttonNumber] = 1;
            buttons[buttonNumber].GetComponent<Image>().color = new Color32(0, 200, 255, 255);
            numberInputField.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            submitButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void selectNumber(int buttonNumber) {
        if (selectButtons[buttonNumber] == 1) {
            resetNumberButton();
        } else {
            resetNumberButton();

            buttons[buttonNumber].GetComponent<Image>().color = new Color32(255, 100, 100, 255);
            selectButtons[buttonNumber] = 1;

            for (int i = 0; i < numbers.Length; i++) {
                if (relasionshipNumbers[buttonNumber, i] == 1) {
                    buttons[i].GetComponent<Image>().color = new Color32(255, 100, 100, 255);
                    selectButtons[i] = 1;
                }
            }
        }
    }

    public void setNumber() {
        setFlag = 0;
        int.TryParse(numberInputField.text, out inputNumber);

        for (int i = 0; i < selectButtons.Length; i++) {
            if (selectButtons[i] == 1 && numbers[i] == inputNumber) {
                setFlag = 1;

                buttonTexts[i].text = inputNumber.ToString();
                inputNumbers[i] = inputNumber;
                descriptionText.text = "Correct answer";

                resetNumberButton();
                resetNumberInputField();
                resetSubmitButton();
            }
        }

        if (setFlag == 0) {
            descriptionText.text = "Wrong number";
        }
    }

    public void okButton() {
        if (modeBoolean[0] == 1) {
            // max mode
            maxNumber = 0;

            for (int i = 0; i < selectButtons.Length; i++) {
                if (selectButtons[i] == 1 & maxNumber < numbers[i]) {
                    maxNumber = numbers[i];
                }
            }

            showResult(maxNumber);
        } else if (modeBoolean[1] == 1) {
            // min mode
            minNumber = 100;

            for (int i = 0; i < selectButtons.Length; i++) {
                if (selectButtons[i] == 1 & minNumber > numbers[i]) {
                    minNumber = numbers[i];
                }
            }

            showResult(minNumber);
        } else {
            // sum mode
            sumNumber = 0;

            for (int i = 0; i < selectButtons.Length; i++) {
                if (selectButtons[i] == 1) {
                    sumNumber = sumNumber + numbers[i];
                }
            }

            showResult(sumNumber);
        }
    }

    public void showResult(int resultNumber) {
        if (modeBoolean[0] == 1) {
            resultText.text = "Max : " + resultNumber.ToString();
        } else if (modeBoolean[1] == 1) {
            resultText.text = "Min : " + resultNumber.ToString();
        } else {
            resultText.text = "Sum : " + resultNumber.ToString();
        }

        moveNumber = moveNumber + 1;
        moveText.text = "Move : " + moveNumber.ToString();
    }

    public async void solvePuzzle() {
        // Debug.Log(clearCheck());
        solveMaxNumber();
        await Task.Delay(500 * (numbers.Length + 1));
        solveMinNumber();
        await Task.Delay(500 * (numbers.Length + 1));
        while (!clearCheck()) {
            solveSumNumber();
            await Task.Delay(500 * (numbers.Length + 1));
        }
    }

    public void selectNumberButton(int i) {
        if (selectButtons[i] == 1) {
            clickNumberButton(i);
            clickNumberButton(i);
        } else {
            clickNumberButton(i);
        }
    }

    public void setCandidate() {
        for (int i = 0; i < numbers.Length; i++) {
            if (countNumbers[i] == 1) {
                for (int j = 0; j < candidateNumber.Length; j++) {
                    if (candidateNumber[j] == i + 1) {
                        clickNumberButton(j);
                        numberInputField.text = candidateNumber[j].ToString();
                        setNumber();
                    }
                }
            }
        }
    }

    public void resetCandidate() {
        for (int i = 0; i < candidateNumber.Length; i++) {
            candidateNumber[i] = 0;
            countNumbers[i] = 0;
        }
    }

    public async void solveMaxNumber() {
        resetCandidate();
        clickModeButton(0);

        for (int i = 0; i < numbers.Length; i++) {
            selectNumberButton(i);
            okButton();
            await Task.Delay(500);

            if (countNumbers[maxNumber - 1] == 0) {
                for (int j = 0; j < numbers.Length; j++) {
                    if ((relasionshipNumbers[i, j] == 1 || i == j) && (candidateNumber[j] == 0 || candidateNumber[j] > maxNumber)) {
                        if (candidateNumber[j] != 0) {
                            countNumbers[candidateNumber[j] - 1]--;
                        }
                        candidateNumber[j] = maxNumber;
                        countNumbers[maxNumber - 1]++;
                    }
                }
            } else {
                for (int j = 0; j < numbers.Length; j++) {
                    if (candidateNumber[j] == maxNumber && !(relasionshipNumbers[i, j] == 1 || i == j)) {
                        candidateNumber[j] = 0;
                        countNumbers[maxNumber - 1]--;
                    }
                }
            }
        }

        clickModeButton(0);
        setCandidate();
    }

    public async void solveMinNumber() {
        resetCandidate();
        clickModeButton(1);

        for (int i = 0; i < numbers.Length; i++) {
            selectNumberButton(i);
            okButton();
            await Task.Delay(500);

            if (countNumbers[minNumber - 1] == 0) {
                for (int j = 0; j < numbers.Length; j++) {
                    if ((relasionshipNumbers[i, j] == 1 || i == j) && (candidateNumber[j] == 0 || candidateNumber[j] < minNumber)) {
                        if (candidateNumber[j] != 0) {
                            countNumbers[candidateNumber[j] - 1]--;
                        }
                        candidateNumber[j] = minNumber;
                        countNumbers[minNumber - 1]++;
                    }
                }
            } else {
                for (int j = 0; j < numbers.Length; j++) {
                    if (candidateNumber[j] == minNumber && !(relasionshipNumbers[i, j] == 1 || i == j)) {
                        candidateNumber[j] = 0;
                        countNumbers[minNumber - 1]--;
                    }
                }
            }
        }

        clickModeButton(1);
        setCandidate();
    }

    public async void solveSumNumber() {
        // sum mode on
        clickModeButton(2);

        for (int i = 0; i < numbers.Length; i++) {
            selectNumberButton(i);
            okButton();
            await Task.Delay(500);

            selectedCount = 0;
            inputCount = 0;
            selectedSumNumber = 0;

            for (int j = 0; j < numbers.Length; j++) {
                if (relasionshipNumbers[i, j] == 1 || i == j) {
                    selectedCount++;
                    selectedSumNumber = selectedSumNumber + inputNumbers[j];

                    if (inputNumbers[j] != 0) {
                        inputCount++;
                    } else {
                        emptyNumber = j;
                    }
                }
            }

            if (selectedCount - inputCount == 1) {
                inputNumbers[emptyNumber] = sumNumber - selectedSumNumber;
                // sum mode off
                clickModeButton(2);
                selectNumberButton(emptyNumber);
                numberInputField.text = inputNumbers[emptyNumber].ToString();
                setNumber();
                // sum mode on
                clickModeButton(2);
            }
        }

        // sum mode off
        clickModeButton(2);
    }

    public bool clearCheck() {
        clear = true;

        for (int i = 0; i < numbers.Length; i++) {
            if (numbers[i] != inputNumbers[i]) {
                clear = false;
            }
        }

        return clear;
    }
}
