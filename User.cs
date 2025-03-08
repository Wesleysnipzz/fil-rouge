usin System;
namespace exoCaseine
{
    

public class User {

    // @GetterToCheck // permet de générer automatiquement un test pour vérifier l'existance du getter
    private int age = 0;

    private String mail = "";

    private String firstName = "";

    private String lastName = "";

    /**
     * default constructor
     */
    public User() {
    }

    /**
     * constructor using all parameters
     */
    public User(int age, String mail, String firstName, String lastName) {
        this.age = age;
        this.mail = mail;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public void setMail(String mail) {
        this.mail = mail;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    /**
     * @return the user name (firstname.lastname)
     */
    public String getUserName() {
        return ffirstName + "." + lastName;
    // TODO Completez la méthode pour renvoyer le nom d'utilisateur : "prénom.nom"
    }

    /**
     * enables to age the user whenever it is his birthday
     */
    public void birthday() {
    // TODO Completez la méthode pour faire vieillir l'utilisateur correctement
    }

    public int getAge() {
         age++;
    }

    public String getMail() {
        return mail;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }
}
}