import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import ListGroupItem from 'react-bootstrap/ListGroupItem';
import { ApolloClient, InMemoryCache, gql } from '@apollo/client';
import Button from 'react-bootstrap/Button';
import { Enrolment } from './Models/Enrolment';
import { EnrollmentService } from './Services/EnrollmentService';
import { StudentService } from './Services/StudentService';
import { Student } from './Models/Student';

interface states {
  id: number,
  localClient: any,
  enrolments: [],
  students: []
}

interface props{

}

export default class App extends React.Component<props, states> {
  constructor(props: props) {
    super(props);
    this.state = {
      id: 1,
      localClient: new ApolloClient({
        uri: 'http://localhost:55194/graphql',
        cache: new InMemoryCache(),
      }),
      enrolments: [],
      students: []
    }
  }

  componentDidMount() {
    this.getEnrollments();
  }

  getEnrollments(){
    this.state.localClient
    .query({
      query: gql`
    query {
      enrollments {
        student {
          firstName
          lastName
          enrollmentDate
        }
        grade
      }
    }
  `
    })
    .then((result: any) => { this.setState({ enrolments: result.data.enrollments.map((x: any) => EnrollmentService.toEnrolment(x)) }); });
  }

  getStudents() {
    this.state.localClient
      .query({
        query: gql`
        query {
          students{
            firstName
            lastName
            enrollmentDate
            enrollments {
              course {
                title
                credits
              }
              grade
            }
          }
        }
    `
      })
      .then((result: any) => {console.log(result.data.students); this.setState({ students: result.data.students.map((x: any) => StudentService.toDifferentStudent(x)) }); });
  }

  getStudentsFiltered() {
    this.state.localClient
      .query({
        query: gql`
        query {
          students (where: {lastName:{contains: "a"}}) {
            firstName
            lastName
            enrollmentDate
            enrollments {
              course {
                title
                credits
              }
              grade
            }
          }
        }
    `
      })
      .then((result: any) => { this.setState({ students: result.data.students.map((x: any) => StudentService.toDifferentStudent(x)) }); });
  }

  getEnrollmentsSorted() {
    this.state.localClient
      .query({
        query: gql`
        query{
          enrollments (where: {  courseId: {eq: 1} }, order:{id: DESC}) {
            id
            student  {
              firstName
              lastName
            }
            course {
              title
              credits
            }
          }
        }
    `
      })
      .then((result: any) => { this.setState({ enrolments: result.data.enrollments.map((x: any) => EnrollmentService.toEnrolment(x)) }); });
  }

  renderEnrolment(enrolment: Enrolment) {
    return (
      <Card style={{ width: '18rem', marginBottom: `1em` }}>
        <Card.Body>
          <Card.Title>Enrollment</Card.Title>
          <Card.Text>
            <b>Grade:</b> {enrolment.Grade}
          </Card.Text>
        </Card.Body>
        <ListGroup className="list-group-flush">
          <ListGroupItem><b>Student Name:</b> {enrolment.Student?.FirstName}</ListGroupItem>
          <ListGroupItem><b>Student LastName:</b> {enrolment.Student?.LastName}</ListGroupItem>
          <ListGroupItem><b>Date:</b> {new Date(enrolment.Student?.EnrolmentDate).toUTCString()}</ListGroupItem>
        </ListGroup>
      </Card>
    );
  }

  renderStudent(student: Student){
    return (
      <Card style={{ width: '18rem', marginBottom: `1em` }}>
        <Card.Body>
          <Card.Title>Student</Card.Title>
          <Card.Text>
            <b>{student.FirstName}</b> {student.LastName}
          </Card.Text>
        </Card.Body>
        <ListGroup className="list-group-flush">
          <ListGroupItem><b>Date:</b> {new Date(student.EnrolmentDate).toUTCString()}</ListGroupItem>
          <ListGroupItem><b>Course title:</b> {(student.Enrolments ? student.Enrolments[0] : null)?.Course?.Title}</ListGroupItem>
          <ListGroupItem><b>Credits:</b> {(student.Enrolments ? student.Enrolments[0] : null)?.Course?.Credits}</ListGroupItem>
        </ListGroup>
      </Card>
    );
  }

  render() {
    return (
      <div className="App">
        <div className="buttons">
          <Button variant="outline-secondary" onClick={() => { this.setState({ id: 1 }); this.getEnrollments();}} disabled={this.state.id === 1}>Enrollments</Button>
          <Button variant="outline-secondary" onClick={() => { this.setState({ id: 2 }); this.getStudents()}} disabled={this.state.id === 2}>Students</Button>
          <Button variant="outline-secondary" onClick={() => { this.setState({ id: 3 }); this.getStudentsFiltered()}} disabled={this.state.id === 3}>Students Filtered</Button>
          <Button variant="outline-secondary" onClick={() => { this.setState({ id: 4 }); this.getEnrollmentsSorted()}} disabled={this.state.id === 4}>Enrollments Sorted</Button>
        </div>
        <div className="cards">
          {this.state.id === 1 || this.state.id === 4 ? 
            this.state.enrolments.map((x: Enrolment) => this.renderEnrolment(x)) :
            this.state.students.map((x: Student) => this.renderStudent(x))}
        </div>
      </div>
    );
  }
}