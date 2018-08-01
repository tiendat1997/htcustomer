<template>
  <div class="hello">
    <button v-on:click="getValue">Get Value</button>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  }, 
  methods: {
    getValue: function(){
      var api = "http://localhost/htcustomer.api/api/values";
      this.axios.get(api).then((response) => {
        console.log(response.data)
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
