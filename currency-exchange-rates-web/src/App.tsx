import { useEffect, useState } from 'react';
import './App.css';
import {
  getCurrentExchangeRates,
  updateExchangeRates,
} from './common/api/exchangeRatesApi';
import { ExchangeRate } from './common/types/type';

function App() {
  const [exchangeRates, setExchangeRate] = useState<ExchangeRate[]>();

  useEffect(() => {
    const fetchExchangeRates = async () => {
      const rates = await getCurrentExchangeRates();
      setExchangeRate(rates);
    };

    fetchExchangeRates();
  }, []);

  const onClick = async () => {
    try {
      const isUpdate = await updateExchangeRates();

      if (isUpdate) {
        const rates = await getCurrentExchangeRates();
        setExchangeRate(rates);
      }

      alert('Exchange rate is up to date');
    } catch (e) {
      alert(e);
    }
  };
  return (
    <div>
      <button onClick={onClick}>Check Updates</button>
      {exchangeRates ? (
        <>
          Date: {exchangeRates.length > 0 && exchangeRates[0].date}
          <br />
          <table>
            <thead>
              <tr>
                <th>Bank Name</th>
                <th>Name</th>
                <th>Code</th>
                <th>Mid</th>
              </tr>
            </thead>
            <tbody>
              {exchangeRates.map((e, index) => {
                return (
                  <tr key={index}>
                    <td>{e.bankName}</td>
                    <td>{e.name}</td>
                    <td>{e.code}</td>
                    <td>{e.mid}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </>
      ) : (
        'No data, please be patient...'
      )}
    </div>
  );
}

export default App;
