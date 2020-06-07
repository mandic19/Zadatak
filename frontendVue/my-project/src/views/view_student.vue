<template>
    <div id="view-student">
        <p class="mt-4"><router-link :to="{ name: 'all_students' }">Return to courses</router-link></p>
        <h4 class="mb-4 mt-4">Student: {{ student.FirstName }} {{ student.LastName }}</h4>

        <table class="table table-hover">
            <thead>
            <tr>
                <td>ID</td>
                <td>Index</td>
                <td>Name</td>
                <td>Year</td>
                <td>Status</td>
            </tr>
            </thead>

            <tbody>
            <tr>
                <td>{{ student.StudentID }}</td>
                <td>{{ student.Index }}</td>
                <td>{{ student.FirstName }} {{ student.LastName }}</td>
                <td>{{ student.Year }}</td>
                <td>{{ student.Status.Name }}</td>
            </tr>
            </tbody>
        </table>
        <table class="table table-hover">
            <thead>
            <tr colspan="5" class="pl-0"><h4>Courses</h4></tr>
            </thead>
            <tbody>
            <tr v-for="course in student.Courses">
                <td>{{ course.CourseID }}</td>
                <td colspan="4">{{ course.Name }}</td>
            </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import UserService from '../services/user.service';

    export default {
        name: 'Home',
        data() {
            return {
                student: [],
            }
        },
        created: function () {
            this.fetchStudentData();
        },
        methods: {
            fetchStudentData: function () {
                let user = JSON.parse(localStorage.getItem('user'));
                UserService.getStudent(user, this.$route.params.id).then((response) => {
                    console.log(response);
                    this.student = response.data;
                }, (response) => {
                });
            },
        }
    }
</script>

