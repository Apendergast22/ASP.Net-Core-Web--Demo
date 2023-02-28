
import 'bootstrap/dist/css/bootstrap.min.css';
import './assets/index.css';

import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App';
import { Provider } from 'react-redux';
import { store } from './services/redux/storage';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>
);
