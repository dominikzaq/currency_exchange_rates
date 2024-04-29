import axios from 'axios';
const VITE_APP_API_ENDPOINT = import.meta.env.VITE_API_ENDPOINT;

const getHeaders = {
  Accept: '*/*',
  'Content-Type': 'application/json',
};

export const client = axios.create({
  withCredentials: true,
  baseURL: VITE_APP_API_ENDPOINT,
  headers: getHeaders,
});


