import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.ParseCancellationException;

public class ConfigErrorStrategy extends DefaultErrorStrategy {

    public ConfigErrorStrategy() {
    }

    public void recover(Parser recognizer, RecognitionException e) {
        for(ParserRuleContext context = recognizer.getContext(); context != null; context = context.getParent()) {
            context.exception = e;
        }

        reportError(recognizer, e);

        throw new ParseCancellationException(e);
    }

    public Token recoverInline(Parser recognizer) throws RecognitionException {
        InputMismatchException e = new InputMismatchException(recognizer);

        for(ParserRuleContext context = recognizer.getContext(); context != null; context = context.getParent()) {
            context.exception = e;
        }

        reportError(recognizer, e);

        throw new ParseCancellationException(e);
    }

    public void sync(Parser recognizer) {
    }

    public void reportError(Parser recognizer, RecognitionException e) {
        if (e instanceof NoViableAltException) {
            this.reportNoViableAlternative(recognizer, (NoViableAltException)e);
        } else if (e instanceof InputMismatchException) {
            this.reportInputMismatch(recognizer, (InputMismatchException)e);
        } else if (e instanceof FailedPredicateException) {
            this.reportFailedPredicate(recognizer, (FailedPredicateException)e);
        } else {
            System.err.println("unknown recognition error type: " + e.getClass().getName());
            recognizer.notifyErrorListeners(e.getOffendingToken(), e.getMessage(), e);
        }
    }
}
