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
    boardsContainer: {
        display: "grid",
        gridTemplateColumns: "12px 1fr 12px",
        gridAutoFlow: "column",
        height: "100%"
    },
    sectionsContainer: {
        height: "100%",
        width: "100%",
        position: "relative"
    },
    sections: {
        display: "grid",
        gridAutoColumns: "minmax(350px, 1fr)",
        gridColumnGap: 8,
        gridAutoFlow: "column",
        position: "absolute",
        width: "100%",
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
                    <div style={styles.boardsContainer}>
                        <div />
                        <div style={styles.sectionsContainer}>
                            <div style={styles.sections}>
                                {_.map(TASK_STATUSES, st => <Section status={st} />)}
                            </div>
                        </div>
                        <div />
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