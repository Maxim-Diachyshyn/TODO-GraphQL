import React, { Component } from 'react';
import _ from "lodash";
import { Scrollbars } from 'react-custom-scrollbars';
import { Grid } from '@material-ui/core';
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";
import Section from "./Section";
import { TASK_STATUSES } from "../../Task/constants"

const styles = {
    scrollContainer: {
        height: "calc(100vh - 84px)",
        marginTop: "64px"
    },
    scrollView: {
    },
    boardsContainer: {
        display: "grid",
        gridTemplateColumns: "12px 1fr 12px",
        gridAutoFlow: "column",
        height: "100%"
    },
    sections: {
        display: "grid",
        gridAutoColumns: "minmax(350px, 1fr)",
        gridColumnGap: 4,
        gridAutoFlow: "column",
    }
}

class Board extends Component {
    renderView = ({ style, ...props }) =>
        <div style={{...style, ...styles.scrollView}} {...props} />

    render() {
        const { id } = this.props.match.params;
        const { isCreating } = this.props;
        return (
            <React.Fragment>
                <TopPanel />
                <Scrollbars style={styles.scrollContainer} renderView={this.renderView}>
                    <div style={styles.boardsContainer}>
                        <div />
                        <div style={styles.sections}>
                            {_.map(TASK_STATUSES, st => <Section status={st} />)}
                        </div>
                        <div />
                    </div>
                </Scrollbars>
                {id ? <UpdateTask id="modal" todoId={id} /> : null}
                {isCreating ? <CreateTask /> : null}
            </React.Fragment>
        )
    }
}

export default Board;