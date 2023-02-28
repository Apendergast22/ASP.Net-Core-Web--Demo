import { configureStore } from '@reduxjs/toolkit';
import { userReducer } from './userSlice';
import { pathReducer } from './pathSlice';

export const store = configureStore({
    reducer: {
        user: userReducer,
        path: pathReducer
    }
});