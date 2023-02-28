import './App.css';

import { BrowserRouter, Route, Routes } from 'react-router-dom'

import { useSelector } from 'react-redux';
import { DRINK, CRIMINALS, SIGNIN, SIGNUP } from './utilites/Paths'
import NavigationBar from './components/NavBar/NavBar'
import Home from './components/Home/Home'
import CheckUserCriminal from './components/CheckUserCriminal/CheckUserCriminal'
import ForbiddenPage from './components/Forbidden/ForbiddenPage'
import CheckUserDrinking from './components/CheckUserDrinking/CheckUserDrinking'
import Footer from './components/Footer/Footer'
import SignIn from './components/SignIn/SignIn'
import SignUp from './components/SignUp/SignUp'

// https://stackoverflow.com/questions/43620289/react-router-cannot-read-property-pathname-of-undefined
const App = () =>{
  
  const logon = useSelector(state => state.user.logon);
  let criminalPage = !logon ? <CheckUserCriminal /> : <ForbiddenPage/>;
  return(
    <BrowserRouter>
      <div className='app-wrapper'>
        <NavigationBar />
        <div className='app-wrapper-content'>
          <Routes>
            <Route index element={<Home />}></Route>
            <Route path={DRINK} element={<CheckUserDrinking />} ></Route>
            <Route path={CRIMINALS} element={criminalPage} ></Route>
            <Route path={SIGNIN} element={<SignIn />} ></Route>
            <Route path={SIGNUP} element={<SignUp />} ></Route>
          </Routes>
        </div>
        <Footer />
      </div>
    </BrowserRouter>
  )
}

export default App;
