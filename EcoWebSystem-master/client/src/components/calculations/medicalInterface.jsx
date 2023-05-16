import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

function MedicalInterface() {
  // Define state variables for the selected form, the arguments and the result
  const [form, setForm] = useState('');
  const [args, setArgs] = useState([]);
  const [result, setResult] = useState('');

  // Define an array of forms with different number of arguments
  const forms = [
    {
      name: 'Оцінка середньої тривалості життя',
      args: 2,
      labels: [
        "Об'єм скидання неочищених стічних вод в поверхневі води",
        'Викиди в атмосфері повітря оксидів азоту',
      ],
    },
    {
      name: 'Оцінка середньої тривалості життя ч.2',
      args: 3,
      labels: [
        'об`єм скидання забруднених стічних вод в поверхневі води',
        'Об`єм викидів в атмосферне повітря хімічних речовин за один рік',
        'Об`єм викидів в атмосферне повітря хімічних речовин за другий',
      ],
    },
    {
      name: 'Прогноз захворювання на гострий інфаркт міокарду',
      args: 4,
      labels: [
        "Об'єм скидання недостатньо очищених стічних вод в поверхневі водні об'єкти",
        'Викиди в атмосферне повітря хімічних речовин пересувними установками',
        'Викиди в атмосферне повітря хімічних речовин стаціонарними установками в останній рік, що бере участь в аналізі',
        'Викиди в атмосферне повітря хімічних речовин стаціонарними установками в попередній рік, що бере участь в аналізі',
      ],
    },
    {
      name: 'Прогноз захворювання на хронічну цереброваскулярну патологію   ',
      args: 2,
      labels: [
        'Викиди в атмосферне повітря хімічних речовин стаціонарними установками в останній рік, що приймає участь в аналізі',
        "Об'єм скидання недостатньо очищених стічних вод в поверхневі водні об'єкти в перший рік, що приймає участь в аналізі",
      ],
    },
  ];

  // Define a function to handle the dropdown change
  const handleSelect = (e) => {
    // Get the value of the selected option
    let value = e.target.value;
    // Set the form state to the selected value
    setForm(value);
    // Reset the args state to an empty array
    setArgs([]);
    // Reset the result state to an empty string
    setResult('');
  };

  // Define a function to handle the input change
  const handleChange = (e) => {
    // Get the index and value of the input element
    let index = e.target.id;
    let value = e.target.value;
    // Copy the args state into a new array
    let newArgs = [...args];
    // Set the element at the index to the input value
    newArgs[index] = value;
    // Set the args state to the new array
    setArgs(newArgs);
  };

  // Define a function to handle the form submission
  const handleSubmit = (e) => {
    // Prevent the default browser behavior
    e.preventDefault();
    // Calculate the result based on the selected form and the arguments
    // You can change this logic to whatever you want
    let res = '';
    switch (form) {
      case 'Оцінка середньої тривалості життя':
        res = Number(args[0]) + Number(args[1]);
        break;
      case 'Оцінка середньої тривалості життя ч.2':
        res = Number(args[0]) * Number(args[1]) * Number(args[2]);
        break;
      case 'Прогноз захворювання на гострий інфаркт міокарду':
        res =
          137.6 +
          0.74 * Number(args[0]) +
          0.68 * Number(args[1]) -
          1.36 * Number(args[2]) +
          1.04 * Number(args[3]);
        break;
      case 'Прогноз захворювання на хронічну цереброваскулярну патологію':
        res = 708 + 1.8 * Number(args[1]) - 0.85 * Number(args[0]);
        break;
      default:
        res = '';
        break;
    }
    // Set the result state to the calculated value
    setResult(res);
  };

  // Define a function to render the inputs based on the selected form
  const renderInputs = () => {
    // Find the form object that matches the selected value
    let formObj = forms.find((f) => f.name === form);
    // If no form is selected, return null
    if (!formObj) return null;
    // Get the number of arguments for the selected form
    let numArgs = formObj.args;
    let labels = formObj.labels;
    // Create an array of inputs with the number of arguments
    let inputs = [];
    for (let i = 0; i < numArgs; i++) {
      console.log('HEY!');
      inputs.push(
        <div className='form-group' key={i}>
          <label htmlFor={i}>{labels[i]}</label>
          <input
            type='number'
            className='form-control'
            id={i}
            value={args[i] || ''}
            onChange={handleChange}
          />
        </div>
      );
    }
    // Return the array of inputs as JSX code
    return inputs;
  };

  // Define a function to render the submit button based on the selected form
  const renderSubmitButton = () => {
    // If no form is selected, return null
    if (!form) return null;
    // Otherwise, return the submit button as JSX code
    return (
      <button type='submit' className='btn btn-primary'>
        Submit
      </button>
    );
  };

  // Define a function to render the result word based on the selected form
  const renderResultWord = () => {
    // If no form is selected, return null
    if (!form) return null;
    // Otherwise, return the result word as JSX code
    return <h3>Результат:</h3>;
  };

  // Return the JSX code for the component
  return (
    <div className='container'>
      <h1 className='text-center'>Лікар</h1>
      <div className='form-group'>
        <label htmlFor='form'>Оберіть обрахунок</label>
        <select
          className='form-control'
          id='form'
          value={form}
          onChange={handleSelect}
        >
          <option value=''>Формула</option>
          {forms.map((f) => (
            <option key={f.name} value={f.name}>
              {f.name}
            </option>
          ))}
        </select>
      </div>
      <form onSubmit={handleSubmit}>
        {renderInputs()}
        {renderSubmitButton()}
      </form>
      <div className='result'>
        {renderResultWord()}
        <h3>{result}</h3>
      </div>
    </div>
  );
}

export default MedicalInterface;
