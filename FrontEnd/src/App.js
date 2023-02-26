import './assets/App.css';
import DrinkForm  from './components/DrinkForm'
import Header from './components/NavBar'
import Homepage from './components/Homepage'
import { Component } from 'react';

// https://stackoverflow.com/questions/43620289/react-router-cannot-read-property-pathname-of-undefined
class App extends Component {
  render() {
    let drawingComponent;
    switch (window.location.pathname){
      case "/":
        drawingComponent = <Homepage />;
        break;
      case "/drinkers":
        drawingComponent = <DrinkForm />;
        break;
        case "/criminals":
          drawingComponent = <DrinkForm />;
          break;
    }
    return(
    <>
      <Header />
      {drawingComponent}
    </>
      )
  }
}

export default App;
