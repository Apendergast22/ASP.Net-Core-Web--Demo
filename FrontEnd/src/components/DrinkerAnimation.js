import vodka_image from'../assets/drink/Vodka.png';
import juice_image from'../assets/drink/Juice.png';

import { View, Image, Text } from 'react-native';
import { useState, useImperativeHandle, forwardRef } from 'react';
import ListGroup from 'react-bootstrap/ListGroup'

const DrinkerAnimation = forwardRef((props, ref) => {
  const [state, setState] = useState({ showVodka: false, showJuice: false });
    
  useImperativeHandle(ref, isDrinker => ({
    StartAnimation(isDrinker) {
      const sleep_for = ms => new Promise(res => setTimeout(res, ms));  
      let step = 100;
      for (let time = 0; time < 5000; time += step){
        setState({ showVodka : time % (2 * step) == 0 })
        setState({ showJuice : time % (2 * step) != 0 })
        sleep_for(step);
      }
        
      setState({ showVodka : isDrinker })
      setState({ showJuice : !isDrinker })
      sleep_for(5000);
      setState({ showVodka : false })
      setState({ showJuice : false })
    }
  }));

    return(
        <View>
          <ListGroup variant="flush" style={{ display: state.showVodka ? "flex" : "none" }}>
            <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={vodka_image} style={{ width:96, height:256 }} ></Image></ListGroup.Item>
            <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>Водка - ваш выбор!!!</Text></ListGroup.Item>
          </ListGroup>
          <ListGroup variant="flush" style={{ display: state.showJuice ? "flex" : "none" }}>
            <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={juice_image} style={{ width:96, height:256 }}></Image></ListGroup.Item>
            <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>По всей видимости, вы не часто пьете...</Text></ListGroup.Item>
          </ListGroup>
        </View>
    );
});

export default DrinkerAnimation;
