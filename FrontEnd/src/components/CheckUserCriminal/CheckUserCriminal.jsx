import './CheckUserCriminal.css';

import React from 'react';
import Button from 'react-bootstrap/Button';
import { sleep_ms } from '../../utilites/CommonUtils';

const CheckUserCriminal = () => {
    const [state, setState] = React.useState( { style:"btn", time: 0 });
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
    <div className='block'>
        <Button className={state.style} onClick={handleClick.bind(this)} type="submit">Проверить</Button>
    </div>);
}

export default CheckUserCriminal;
