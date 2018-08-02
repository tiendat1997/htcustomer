<template>   
    <div class="container text-center signin-container">
        <b-form @submit="onSubmitLogin" @reset="onResetLogin" class="signin-form">
            <img src="../assets/logo.png" alt="image" width="72" height="72" />        
            <h1 class="h3 mb-3 font-weight-normal">Huy Thong</h1>
            <b-form-group>
                <b-form-input v-model="form.email" type="email" required placeholder="Enter email"></b-form-input>                
            </b-form-group>
            <b-form-group>
                <b-form-input v-model="form.password" type="password" required placeholder="Enter password"></b-form-input>
            </b-form-group>
                <b-button type="submit" variant="primary">Sign In</b-button>
            <p class="mt-5 mb-3 text-muted">Register Now</p>
        </b-form>        
    </div>
</template>

<script>
export default {
    name: 'Login',
    data: function() {
        return {
            form: {
                email: '',
                password: '',
                grant_type: 'password'
            }
        }
  },
  methods: {
        onSubmitLogin(evt) {
            evt.preventDefault();
            var uri = 'http://localhost/htcustomer.api';
            var api = this.axios.create({baseURL: uri});
            var self = this;
            api.post('/token', {
                username: self.email,
                password: self.password,
                grant_type: self.grant_type    
            })
            .then((response) => {
                console.log(response);
            })
        },
        onResetLogin(evt) {
            evt.preventDefault();
        }
  }
}
</script>

<style scoped>   
    .signin-container {
        text-align: center !important;
        height: 100vh;
        justify-content: center;
        align-items: center;
        display: flex;
        padding-top: 40px;
        padding-bottom: 40px;
    }
   .signin-form{
        width: 100%;
        max-width: 330px;
        padding: 15px;
        margin: 0 auto;
        background-color: #f5f5f5;
   }
   
</style>



