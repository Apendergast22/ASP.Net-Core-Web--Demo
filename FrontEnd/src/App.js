import './assets/App.css';
import DrinkForm  from './components/DrinkForm'
import Header from './components/NavBar'
import Homepage from './components/Homepage'
import { Component } from 'react';
import { Router, Route, Routes } from 'react-router';

// https://stackoverflow.com/questions/43620289/react-router-cannot-read-property-pathname-of-undefined
class App extends Component {
  render() { 
    return(
      <Router>
        <Header/>
        <Routes>
          <Route path="/" element={<Homepage/>}/>
          <Route path="/drinkers" element={<DrinkForm/>}/>
          <Route path="/criminals" element={<DrinkForm/>}/>
        </Routes>
      </Router>
      )
  }
}

export default App;
