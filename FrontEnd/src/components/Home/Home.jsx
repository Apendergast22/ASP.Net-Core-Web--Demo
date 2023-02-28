import React from 'react'
import { Markup } from 'interweave';
import { FIRST_HTML, SECOND_HTML, THIRD_HTML, FOURTH_HTML } from './HTML'

const Home = () => {
    return (
        <div>
            <Markup content={FIRST_HTML}/>
            <Markup content={SECOND_HTML}/>
            <Markup content={THIRD_HTML}/>
            <Markup content={FOURTH_HTML}/>
        </div>
    );
}

export default Home;