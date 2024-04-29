import { client } from '../helpers/httpCommon';
import { ExchangeRate } from '../types/type';

const url = '/exchangerate';

export const getCurrentExchangeRates = async () => {
  const response = await client.get<ExchangeRate[]>(url);
  return response.data;
}

export const updateExchangeRates = async () => {
  const response = await client.post<ExchangeRate[]>(url);
  return response.data;
}