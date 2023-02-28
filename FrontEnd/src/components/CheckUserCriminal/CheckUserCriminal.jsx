import './CheckUserCriminal.css';

import React from 'react';
import { Text } from 'react-native';

import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';

import { sleep_ms } from '../../utilites/CommonUtils';

const CheckUserCriminal = () => {
    const [state, setState] = React.useState( { style:"btn", time: 0 });
    React.useEffect(() => {
        if (state.time !== 0){
            setState({ time:0, style:"btn" }); 
        }
     }, [state]);

    const handleClick = () => {
        const colors = ["btn-gold", "btn-purple", "btng-green", "btn"];
        const step = 500;
        let count = 0;
        for (let i = 0; i < 25000; i += step){
            setState({ time:state.time + 1, style:colors[count] });

            sleep_ms(step);
            count = count < colors.length ? count + 1 : 0;
          }

    }

    return(
    <div>
        <Container>
            <Button className={state.style} onClick={handleClick.bind(this)} type="submit">Проверить</Button>
        </Container>
    </div>);
}

export default CheckUserCriminal;
