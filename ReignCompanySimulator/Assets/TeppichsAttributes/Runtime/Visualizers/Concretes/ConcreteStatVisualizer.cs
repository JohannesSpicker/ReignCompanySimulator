using System.Collections.Generic;
using TeppichsAttributes.Modifiers;
using TeppichsAttributes.Visualizers.Primitives;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TeppichsAttributes.Visualizers.Concretes
{
    public class ConcreteStatVisualizer : StatVisualizer
    {
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textMinValue;
        [SerializeField] private TMP_Text textMaxValue;
        [SerializeField] private TMP_Text textBaseValue;
        [SerializeField] private TMP_Text textValue;

        [SerializeField] private Image image;


        protected override void DisplayName(string inGameName)
        {
            textName.text = inGameName;
        }

        protected override void DisplaySprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        protected override void DisplayMinValue(float minValue)
        {
            textMinValue.text = minValue.ToString();
        }

        protected override void DisplayMaxValue(float maxValue)
        {
            textMaxValue.text = maxValue.ToString();
        }

        protected override void DisplayBaseValue(float baseValue)
        {
            textBaseValue.text = baseValue.ToString();
        }

        protected override void DisplayValue(float value)
        {
            textValue.text = value.ToString();
        }

        protected override void DisplayModifiers(IEnumerable<Modifier> modifiers)
        {
            //TODO: display modifiers
        }
    }
}