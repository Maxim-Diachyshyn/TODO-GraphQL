import React, { Component } from 'react';
import _ from "lodash";
import { Grid } from '@material-ui/core';
import PerfectScrollbar from 'react-perfect-scrollbar'
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";
import Section from "./Section";
import { TASK_STATUSES } from "../../Task/constants"

const styles = {
    scrollContainer: {
        height: "calc(100vh - 68px)",
        marginTop: "64px"
    },
    sections: {
        display: "grid",
        gridAutoColumns: "minmax(350px, 1fr)",
        gridColumnGap: 8,
        gridAutoFlow: "column",
        height: "100%"
    },
    modalContainer: {
        top: 0,
        left: 0,
        position: "absolute",
        height: "100%",
        width: "100%"
    }
}

class Board extends Component {
    render() {
        const { id } = this.props.match.params;
        const { isCreating } = this.props;
        return (
            <React.Fragment>
                <TopPanel />
                <PerfectScrollbar style={styles.scrollContainer} options={{ suppressScrollY: true }}>
                    <div style={styles.sections}>
                        {_.map(TASK_STATUSES, st => <Section status={st} />)}
                    </div>
                </PerfectScrollbar>
                {id || isCreating ? (
                <div style={styles.modalContainer}>
                    {id ? <UpdateTask id="modal" todoId={id} /> : null}
                    {isCreating ? <CreateTask /> : null}
                </div>
                ) : null}

            </React.Fragment>
        )
    }
}

export default Board;