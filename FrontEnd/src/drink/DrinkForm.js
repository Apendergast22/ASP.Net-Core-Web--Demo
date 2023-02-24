import './DrinkForm.css';
import vodka_image from'../resources/Vodka.png';
import juice_image from'../resources/Juice.png';
import * as React from 'react';
import { View, Image } from 'react-native';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Stack from 'react-bootstrap/Stack';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Text from 'react-bootstrap/Col';
import ListGroup from 'react-bootstrap/ListGroup';

// https://mui.com/material-ui/react-stack/
// https://react-bootstrap.github.io/layout/stack/
class DrinkForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = { surname: "", name: "", patronymic: "", birthDay: "", showVodka: false, showJuice: false };
    this.sleep = ms => new Promise(res => setTimeout(res, ms));
  }
  
  parseErrorMessage(requestJson) {
    let targetProperty = "";
    if (requestJson["instance"] == "Name"){
      targetProperty = "Имя";
    } else if (requestJson["instance"] == "Surname"){
      targetProperty = "Фамилия";
    }

    if (requestJson["detail"] == ""){
      return "Поле \'" + targetProperty + "\' не может быть пустым!";
    }
    else{
      return "\'" + targetProperty + "\' имеет некорректное значение!";
    }
  }

  async postData() {
    let config = require('../config.json');
    let url = config.use_tls ? config.server_url_ssl + "/Drinkers" : config.server_url + "/Drinkers";
    let request = new Request(url, {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(this.state)
    });

    let response = await fetch(request);
    if (response.ok){
      let result = await response.json();

      let step = 100;
      for (let time = 0; time < 5000; time += step)
      {
        this.setState({ showVodka : time % (2 * step) == 0 })
        this.setState({ showJuice : time % (2 * step) != 0 })
        await this.sleep(step);
      }
      
      this.setState({ showVodka : result["doesUserDrinker"] })
      this.setState({ showJuice : !result["doesUserDrinker"] })
      await this.sleep(5000);
      this.setState({ showVodka : false })
      this.setState({ showJuice : false })
    }
    else{
      var result = await response.json();
      alert(this.parseErrorMessage(result));
    }
  }

  render() {
    const postDataLambda = async () => await this.postData();
    return (
      <div className="DrinkForm">
        <Stack direction="horizontal" gap={3}>
          <Col sm={5}>
            <Form>
              <Form.Group as={Row} className="mb-3" controlId="surname">
                <Form.Label as="legend" column sm={0}>Фамилия: </Form.Label>
                <Col sm={8}><Form.Control type="text" value={this.state.surname} onChange={e =>  this.setState({ surname: e.target.value }) } placeholder="Введите вашу фамилию..." /></Col>
              </Form.Group>
              <Form.Group as={Row} className="mb-3" controlId="name">
                <Form.Label as="legend" column sm={0}>Имя: </Form.Label>
                <Col sm={8}><Form.Control type="text" value={this.state.name} onChange={e =>  this.setState({ name: e.target.value }) } placeholder="Введите ваше имя..." /></Col>
              </Form.Group>
              <Form.Group as={Row} className="mb-3" controlId="patronymic">
                <Form.Label as="legend" column sm={0}>Очество: </Form.Label>
                <Col sm={8}><Form.Control type="text" value={this.state.patronymic} onChange={e =>  this.setState({ patronymic: e.target.value }) } placeholder="Введите ваше очество..." /></Col>
              </Form.Group>
              <Form.Group as={Row} className="mb-3" controlId="birthday">
                <Form.Label as="legend" column sm={0}>Год рождения: </Form.Label>
                <Col sm={8}><Form.Control type="date" value={this.state.birthDay} onChange={e =>  this.setState({ birthDay: e.target.value }) }/></Col>
              </Form.Group>
              <Col align="right"><Button as="a" variant="success" value="Submit" onClick = {postDataLambda} >Отправить данные</Button></Col>
            </Form>
          </Col>
          <Col sm={5}>
            <View>
              <ListGroup variant="flush" style={{ display: this.state.showVodka ? "flex" : "none" }}>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={vodka_image} style={{ width:96, height:256 }} ></Image></ListGroup.Item>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>Водка - ваш выбор!!!</Text></ListGroup.Item>
              </ListGroup>
              <ListGroup variant="flush" style={{ display: this.state.showJuice ? "flex" : "none" }}>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={juice_image} style={{ width:96, height:256 }}></Image></ListGroup.Item>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>По всей видимости, вы не часто пьете...</Text></ListGroup.Item>
              </ListGroup>
            </View>
          </Col>
        </Stack>
      </div>
    );
  }
}

export default DrinkForm;