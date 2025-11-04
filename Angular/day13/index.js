const students =[
  {
    "FirstName": "John",
    "LastName": "Doe",
    "Age": 20,
    "Department": "Computer Science"
  },
  {
    "FirstName": "Jane",
    "LastName": "Smith",
    "Age": 22,
    "Department": "Physics"
  },
  {
    "FirstName": "Michael",
    "LastName": "Johnson",
    "Age": 21, 
    "Department": "Mathematics"
  },
  {
    "FirstName": "Sarah",
    "LastName": "Williams",
    "Age": 19,
    "Department": "Computer Science"
  },
  {
    "FirstName": "Robert",
    "LastName": "Brown",
    "Age": 23,
    "Department": "Mathematics"
  },
  {
    "FirstName": "Emily",
    "LastName": "Davis",
    "Age": 20,
    "Department": "Computer Science"
  }
]

//1. List the students whose department is computer science.

const csStudents = students.filter(student => student.Department === "Computer Science");
console.log("Computer Science Students:", csStudents);

//2. List the first name of students whose age is greater than  21

const ageGreaterThan20 = students.filter(student => student.Age > 20);
console.log("Students with Age greater than 20:", ageGreaterThan20);

//3. Check whether a student having a first name as Robert is present in the Computer Science Department. The result should be in boolean type

const isRobertInCS = students.some(student => student.FirstName === "Robert" && student.Department === "Computer Science");
console.log("Is Robert in Computer Science Department?", isRobertInCS);

//4. Check whether there is any student whose age is greater than 23 is studying in the Maths department.The result should be in boolean type

const isAgeGreaterThan23InMaths = students.some(student => student.Age > 23 && student.Department === "Mathematics");
console.log("Is there any student older than 23 in Mathematics Department?", isAgeGreaterThan23InMaths);

//5. Check whether all the students are above an age group of 18.The result should be in boolean type.

const areAllStudentsAbove18 = students.every(student => student.Age > 18);
console.log("Are all students above 18?", areAllStudentsAbove18);

//6. Assuming that there is only one student having a first name as John, Print his department name.
const john = students.find(student => student.FirstName === "John");
if (john) {
  console.log("John's Department:", john.Department);
}



const movies =
[
  {
    "MovieName": "The Great Adventure",
    "ActorName": "John Smith",
    "ReleaseDate": "2023-01-15"
  },
  {
    "MovieName": "Mystery in the Woods",
    "ActorName": "Emily Johnson",
    "ReleaseDate": "2022-09-28"
  },
  {
    "MovieName": "Love and Destiny",
    "ActorName": "Michael Brown",
    "ReleaseDate": "2023-05-02"
  },
  {
    "MovieName": "City of Shadows",
    "ActorName": "Sophia Williams",
    "ReleaseDate": "2023-03-12"
  },
  {
    "MovieName": "The Last Stand",
    "ActorName": "William Davis",
    "ReleaseDate": "2022-11-07"
  },
  {
    "MovieName": "Echoes of Time",
    "ActorName": "Olivia Wilson",
    "ReleaseDate": "2022-12-19"
  }
]

//1. List the movie name along with the actor name of those movies released in the year 2022

const list = movies.filter(movie => {
  return new Date(movie.ReleaseDate).getFullYear() === 2022
}).map(movie => ({ MovieName: movie.MovieName, ActorName: movie.ActorName }));

console.log("Movies released in 2022:", list);

//2. List the movie names released in the year 2023 where the actor is William Davis.

const williamMovies2023 = movies.filter(movie => {
  return new Date(movie.ReleaseDate).getFullYear() === 2023 && movie.ActorName === "William Davis"
}).map(movie => movie.MovieName);

console.log("Movies released in 2023 with William Davis:", williamMovies2023);

//3. Retrieve the Actor name and release date of the movie “The Last Stand”

const lastStand = movies.find(movie => movie.MovieName === "The Last Stand");
if (lastStand) {
  console.log("The Last Stand - Actor:", lastStand.ActorName, ", Release Date:", lastStand.ReleaseDate);
}


const movieOfJohnDoe = movies.find(movie => movie.ActorName === "John Doe");
if (movieOfJohnDoe) {
  console.log("Movie of John Doe:", movieOfJohnDoe.MovieName);
}

//5. Display the count of movies where the actor name is "Sophia Williams"
const numberOfSophiaMovies = movies.filter(movie => movie.ActorName === "Sophia Williams").length;
console.log("Count of movies with Sophia Williams:", numberOfSophiaMovies);

movies.push({
   			 "MovieName": "The Final Stage",
    			"ActorName": "John Doe",
   	 		"ReleaseDate": "2022-08-11"
 		 });

     console.log("Updated Movies List:", movies);

     //7. Check whether there exists any duplicate movie names present in the array
     const hasDuplicate = movies.map(movie => movie.MovieName).some((name, index, arr) => arr.indexOf(name) !== index);
     console.log("Are there duplicate movie names?", hasDuplicate);

     //8. Create a new array starting from the movie "City of Shadows"

     const cityOfShadowsIndex = movies.findIndex(movie => movie.MovieName === "City of Shadows");
     const newArrayFromCityOfShadows = cityOfShadowsIndex !== -1 ? movies.slice(cityOfShadowsIndex) : [];
     console.log("New array from 'City of Shadows':", newArrayFromCityOfShadows);

//9. List the distinct actor names in array
     const distinctNames  = movies.filter((movie,index,arr) => arr.indexOf(movie)===index ).map(m => m.ActorName);
     console.log("DIstinct actors are : ",distinctNames);


     /*10. Insert an element
		{
   			 "MovieName": "Rich & Poor",
    			"ActorName": "Johnie Walker",
   	 		"ReleaseDate": "2023-08-11"
 		 }
	as next element to movie “Love and Destiny”*/
      const loveAndDestinyIndex = movies.findIndex(movie => movie.MovieName === "Love and Destiny");
      if (loveAndDestinyIndex !== -1) {
        movies.splice(loveAndDestinyIndex + 1, 0, {
          "MovieName": "Rich & Poor",
          "ActorName": "Johnie Walker",
          "ReleaseDate": "2023-08-11"
        });
      }
      console.log("Movies after insertion:", movies);

      //11. Display the count of distinct actor names in array

      const distinctActorCount = new Set(movies.map(movie => movie.ActorName)).size;
      console.log("Count of distinct actor names:", distinctActorCount);

      //12. Remove the movie named  "The Last Stand"
      const lastStandIndex = movies.findIndex(movie => movie.MovieName === "The Last Stand");
      if (lastStandIndex !== -1) {
        movies.splice(lastStandIndex, 1);
      }
      console.log("Movies after removal:", movies);


//13. Check whether all the movies are released after 2021 Dec 31

const allAfter2021Dec31 = movies.every(movie => new Date(movie.ReleaseDate) > new Date('2021-12-31'));
console.log("Are all movies released after 2021 Dec 31?", allAfter2021Dec31);

//14. Update movie named  "City of Shadows" ‘s release date as  "2023-03-13"
const cityOfShadows = movies.find(movie => movie.MovieName === "City of Shadows");
if (cityOfShadows) {
  cityOfShadows.ReleaseDate = "2023-03-13";
}
console.log("Movies after update:", movies);

//15. Create a new array of movie names whose movie name length is greater than 10
const longMovieNames = movies.filter(movie => movie.MovieName.length > 10).map(movie => movie.MovieName);
console.log("Movies with names longer than 10 characters:", longMovieNames);