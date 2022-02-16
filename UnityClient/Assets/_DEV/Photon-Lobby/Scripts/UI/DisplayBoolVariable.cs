using Platformer.Utils;
using UnityEngine;

namespace Platformer.UI
{
    public class DisplayBoolVariable : TextElement
    {
        [SerializeField] private BoolVariable _boolVariable;
        [SerializeField] private string _trueReplacement;
        [SerializeField] private string _falseReplacement;
        [SerializeField] private string _context;

        private void OnEnable()
        {
            if(_boolVariable) _boolVariable.onValueChanged += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            if(_boolVariable) _boolVariable.onValueChanged -= UpdateText;
        }
    
        private void Start()
        {
            UpdateText();
        }
    
        private void UpdateText()
        {
            if (_context == "")
            {
                ThisText.text = _boolVariable.Value ? _trueReplacement : _falseReplacement;
            }else
            {
                ThisText.text =  string.Format(_context, _boolVariable.Value ? _trueReplacement : _falseReplacement);
            }
        }
    }

}