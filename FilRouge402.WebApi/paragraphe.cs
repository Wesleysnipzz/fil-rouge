package fr.uha40.Elements;

import caseine.format.javajunit.Grade;

/**
 * 	/!\ Attentions !
 * 	Tant que tous les constructeurs demandés ne sont pas rédigés ; l'evaluation du projet par caseine n'est pas possible
 * 	L'outil employé pour générer l'exercice ne permet pas d'afficher les questions dans l'ordre sur le fichier. Lisez tout et réalisez les dans l'ordre
 */
public class Paragraphe {

    /**
     * la chaine de caractère attendue pour initialiser le style vide dans la balise html
     */
    protected final static String STYLE = "style=\"\"";

    private String text = "";

    /**
     * Permet d'initialiser un paragraphe avec un texte
     * @param text : le texte que contiendra le paragraphe
     */
    public Paragraphe(String text) {
        // TODO (2) Implémenter le constructeur valuée qui permet de générer un paragraphe avec un texte
    }

    /**
     * Permet d'initialiser un paragraphe avec un texte répété X fois. Chaque répétition est séparée par un espace (à la fin de la phrase)
     * @param text
     * @param x
     */
    public Paragraphe(String text, int x) {
        // TODO (3) Implémenter le constructeur valuée qui permet de générer un paragraphe avec un texte répété X fois
    }

    public void setText(String text) {
        this.text = text;
    }

    public String getText() {
        return text;
    }

    @Override
    public String toString() {
        return "Paragraphe [text=" + text + "]";
    }
    // TODO (1) Déclarer un constructeur par défaut qui créé un paragraphe sans texte
    // 
    // TODO (4) Implementer le constructeur par recopie
    // 
}