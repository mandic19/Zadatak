<template>
    <div id="view-student" class="mt-4">
        <p><router-link :to="{ name: 'Course' }">Return to courses</router-link></p>
        <h4 class="mb-4 mt-4">Course: {{ course.Name }}</h4>

        <table class="table table-hover">
            <thead>
            <tr>
                <td>ID</td>
                <td>Index</td>
                <td>Name</td>
                <td>Year</td>
            </tr>
            </thead>
            <tbody>
            <tr v-for="student in course.Students">
                <td>{{ student.StudentID }}</td>
                <td>{{ student.Index }}</td>
                <td>{{ student.FirstName }} {{ student.LastName }}</td>
                <td>{{ student.Year }}</td>
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
                course: [],
            }
        },
        created: function () {
            this.fetchCourseData();
        },
        methods: {
            fetchCourseData: function () {
                let user = JSON.parse(localStorage.getItem('user'));
                UserService.getCourse(user, this.$route.params.id).then((response) => {
                    this.course = response.data;
                }, (response) => {
                });
            },
        }
    }
</script>

